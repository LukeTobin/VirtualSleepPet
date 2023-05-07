using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public enum State{
        Home,
        Game,
        Cosmetics
    }

    public State state;

    void Awake() {
        Instance = this;    
    }

    public void ChangeState(int newState) {
        switch(newState){
            case 0: 
                state = State.Home;
                break;
            case 1: 
                state = State.Game;
                break;
            case 2:
                state = State.Cosmetics;
                break;
            default:
                state = State.Home;
                break;
        }
    }
}
