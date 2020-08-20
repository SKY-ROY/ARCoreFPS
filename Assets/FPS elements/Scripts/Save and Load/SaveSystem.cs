using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveState(GlobalSettingsControl myScene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/this.Sett";
        FileStream stream = new FileStream(path, FileMode.Create);

        StateData data = new StateData(myScene);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static StateData LoadState()
    {
        string path = Application.persistentDataPath + "/this.Sett";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StateData data = formatter.Deserialize(stream) as StateData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("SAVE file not found in" + path);
            return null;
        }
    }
    public static void SaveInventory(MyInventoryControl inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/this.Inv";
        FileStream stream = new FileStream(path, FileMode.Create);

        StateData data = new StateData(inventory);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static StateData LoadInventory()
    {
        string path = Application.persistentDataPath + "/this.Inv";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StateData data = formatter.Deserialize(stream) as StateData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("SAVE file not found in" + path);
            return null;
        }
    }
}