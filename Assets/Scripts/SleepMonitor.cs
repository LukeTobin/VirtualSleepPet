using System;
using UnityEngine;
using TMPro;

public class SleepMonitor : MonoBehaviour
{
    public static SleepMonitor Instance;

    [Header("Popup First Time")]
    [SerializeField] Popup popup;
    [SerializeField] PopupButton btnPrefab;
    [SerializeField] string ft_header;
    [SerializeField] [TextArea] string ft_desc;

    [Header("Debug")]
    [SerializeField] bool reset;
    [SerializeField] bool setSleepHours;
    [SerializeField] bool isNightTime;
    [SerializeField] int customHours;
    [SerializeField] int customMins;
    [Space]
    [SerializeField] SliderModifier slider;
    
    [Header("Color Settings")]
    [SerializeField] Color goodSleep = Color.green;
    [SerializeField] Color badSleep = Color.red;

    [Header("Animation Triggers")]
    [SerializeField] string happyTrigger;
    [SerializeField] string angryTrigger;
    [SerializeField] string sleepyTrigger;
    [SerializeField] string resetTrigger;
    [SerializeField] Animator animator;

    [Header("Refs")]
    [SerializeField] TMP_Text targetSleepText;

    TimeSpan savedTime;
    bool shouldBeAngry;

    public TimeSpan TargetSleep {get; private set;}

    void Awake(){
        Instance = this;
        TargetSleep = new TimeSpan(PlayerPrefs.GetInt("TargetSleepHours", -1), PlayerPrefs.GetInt("TargetSleepMinutes", -1), 0);
    }

    void Start()
    {
        if(reset) PlayerPrefs.DeleteAll();
        UpdateLastOpenTime();
        if(TargetSleep.Hours != -1 && TargetSleep.Minutes != -1){
            targetSleepText.text = $"Target Sleep [{TargetSleep.Hours}:{TargetSleep.Minutes}]";
        }
    }

    public void UpdateLastOpenTime(){
        // Get the last real time that the game was opened
        string lastOpenedTimeString = PlayerPrefs.GetString("LastOpenedTime", string.Empty);
        TimeSpan timeDifference = new TimeSpan();

        DateTime _currentTime = DateTime.Now;
        if(_currentTime.Hour >= 21 || _currentTime.Hour <= 4) isNightTime = true;

        if (lastOpenedTimeString != string.Empty)
        {
            DateTime lastOpenedTime = DateTime.Parse(lastOpenedTimeString);       
            timeDifference = _currentTime - lastOpenedTime;

            if(setSleepHours && !reset) {
                timeDifference = new TimeSpan(customHours, customMins, 0);
            }

            print(timeDifference);
            QueryTime(timeDifference);
        }
        else
        {
            print("First time opening, going to setup");

            ForceHappyState();

            PopupButton btn = Instantiate(btnPrefab);
            btn.Text.text = "Continue";
            btn.Btn.onClick.AddListener(() => popup.Close());
            btn.Btn.onClick.AddListener(() => SetupTargetSleepTime());

            popup.CreatePopup(ft_header, ft_desc, btn);
            popup.gameObject.SetActive(true);
        }

        if(TargetSleep.Hours == -1 || TargetSleep.Minutes == -1){
            SetupTargetSleepTime();
        }

        // Save the current real time as the last opened time
        DateTime currentTime = DateTime.Now;
        PlayerPrefs.SetString("LastOpenedTime", currentTime.ToString());
        PlayerPrefs.Save();
    }

    public void QueryTime(TimeSpan timeSlept){
        savedTime = timeSlept;

        if(timeSlept.Hours >= 1 && timeSlept.Hours <= 5){
            PopupButton btn = Instantiate(btnPrefab);
            btn.Text.text = "Continue";
            btn.Btn.onClick.AddListener(() => popup.Close());
            btn.Btn.onClick.AddListener(() => SleepConfirmed(timeSlept));

            slider.SliderUpdate(25);
            shouldBeAngry = true;

            popup.CreatePopup("Welcome Back!", "You've slept for... <color=red>" + timeSlept.Hours + "</color> Hours!\n\n" +
                                                "That's a very low amount of sleep for you and your pet. You may notice a drop-off in your energy early in the day.", btn, true);
            popup.gameObject.SetActive(true);
        }
        else if(timeSlept.Hours >= 6 && timeSlept.Hours <= 9){
            PopupButton btn = Instantiate(btnPrefab);
            btn.Text.text = "Continue";
            btn.Btn.onClick.AddListener(() => popup.Close());
            btn.Btn.onClick.AddListener(() => SleepConfirmed(timeSlept));

            slider.SliderUpdate(95);
            shouldBeAngry = false;

            popup.CreatePopup("Welcome Back!", "You've slept for... <color=green>" + timeSlept.Hours + "</color> Hours!\n\n" +
                                                "That's a great amount of sleep for you and your pet! Keep up the good work!", btn, true);
            popup.gameObject.SetActive(true);
        }
        else if(timeSlept.Hours >= 10 && timeSlept.Hours <= 14){
            PopupButton btn = Instantiate(btnPrefab);
            btn.Text.text = "Continue";
            btn.Btn.onClick.AddListener(() => popup.Close());
            btn.Btn.onClick.AddListener(() => SleepConfirmed(timeSlept));

            slider.SliderUpdate(60);
            shouldBeAngry = false;

            popup.CreatePopup("Welcome Back!", "You've slept for... <color=red>" + timeSlept.Hours + "</color> Hours!\n\n" +
                                                "That's a very high amount of sleep for you and your pet. You may feel sluggish and tired throughout your morning. Try out some of your pet's tips tonight!.", btn, true);
            popup.gameObject.SetActive(true);
        }
        else if(timeSlept.Hours >= 15 || timeSlept.Days >= 1){
            // cant track sleep properly
            popup.CreatePopup("Welcome Back!", "We couldn't track your sleep last night, you can manually adjust your sleep time below! ", null, true);
            popup.gameObject.SetActive(true);
            shouldBeAngry = false;
        }

        TriggerAnimations();
    }

    public void SleepConfirmed(TimeSpan timeSpan){
        SaveData.current.Load();
        SleepData data = SaveData.current.SleepData;

        DateTime yesterdayDate = DateTime.Today.AddDays(-1);
        int yesterday = yesterdayDate.Day;

        if(data.Days[yesterday] == null){
            data.Days[yesterday] = new Vector2(-Mathf.Abs(timeSpan.Hours - 8),Mathf.Abs(timeSpan.Hours -8));
            SaveManager.Instance.Save(data);
            return;
        }
        
        float x = data.Days[yesterday].x;
        float y = data.Days[yesterday].y;

        if(timeSpan.Hours < 8){
            float _x = Mathf.Abs(Mathf.Abs(x) - 8);
            x = Mathf.Abs(((_x + timeSpan.Hours) / 2) - 8);
        }
        else if(timeSpan.Hours > 8){
            float _y = Mathf.Abs(y + 8);
            y = Mathf.Abs(((_y + timeSpan.Hours) / 2) - 8);
        }
        else{
            if(x != 0) x /= 2;
            if(y != 0) y /= 2;
        }

        if(timeSpan.Hours == 8){
            LevelContainer.Instance.SleepQuality(2);
        }else if(timeSpan.Hours <= 9 && timeSpan.Hours >= 7){
            LevelContainer.Instance.SleepQuality(1);
        }
        else{
            LevelContainer.Instance.SleepQuality(0);
        }

        data.Days[yesterday] = new Vector2(-x,y);
        SaveManager.Instance.Save(data);
    }

    public void SetupTargetSleepTime(){
        PopupButton btn = Instantiate(btnPrefab);
        btn.Text.text = "Set";
        btn.Btn.onClick.AddListener(() => GetTime.Instance.ShowTimePicker());
        btn.Btn.onClick.AddListener(() => popup.Close());

        popup.CreatePopup("Set Target Sleep", "Set what time you want to aim to fall asleep around. This lets us adjust the app to your needs and tailor your growth towards these goals.", btn);
        popup.gameObject.SetActive(true);
    }

    public void TriggerAnimations(){
        if(DateTime.Now.Hour >= 21 || DateTime.Now.Hour <= 5 || isNightTime){
            animator.SetTrigger(sleepyTrigger);
        }
        else if(shouldBeAngry){
            animator.SetTrigger(angryTrigger);
        }
        else{
            animator.SetTrigger(happyTrigger);
        }
    }

    public void AnimatorDefaultState(){
        animator.SetTrigger(resetTrigger);
    }

    public void ForceHappyState(){
        animator.SetTrigger(happyTrigger);
    }

    public void SetTargetSleepTime(int hour, int min){
        TargetSleep = new TimeSpan(hour, min, 0);
        PlayerPrefs.SetInt("TargetSleepHours", hour);
        PlayerPrefs.SetInt("TargetSleepMinutes", min);
        PlayerPrefs.Save();
        targetSleepText.text = $"Target Sleep [{TargetSleep.Hours}:{TargetSleep.Minutes}]";
    }
}
