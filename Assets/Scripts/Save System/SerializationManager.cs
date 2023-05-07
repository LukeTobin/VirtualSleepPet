using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;

public class SerializationManager
{
    public static bool Save(string saveName, object saveData){
        // Built in formatter
        BinaryFormatter formatter = GetBinaryFormatter();

        // Validate directory
        if(!Directory.Exists(Application.persistentDataPath + "/data")){
            Directory.CreateDirectory(Application.persistentDataPath + "/data");
        }

        // Create & serialize file
        string path = Application.persistentDataPath + "/data/" + saveName + ".save";
        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();

        return true;
    }

    public static object Load(string path){
        // guard claus
        if(!File.Exists(path)) return null;

        // Access FileStream to find out a file based on the given path
        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try{
            // Try Deserialize the file
            object save = formatter.Deserialize(file);
            // If we could, close the file
            file.Close();
            return save;
        }
        catch{
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter(){
        BinaryFormatter formatter = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();

        VectorSerializationSurrogate vectorSerialization = new VectorSerializationSurrogate();
        selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vectorSerialization);

        formatter.SurrogateSelector = selector;

        return formatter;
    }
}
