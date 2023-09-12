using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    
    public static void SaveGoc (GameOverController goc) {
        BinaryFormatter formatter = new BinaryFormatter();
        string pathh = "gamedata.txt";
        string path = System.IO.Path.Combine(Application.persistentDataPath, pathh);
        FileStream stream = new FileStream(path, FileMode.Create);
        stream.Position = 0;

        PlayerData data = new PlayerData(goc);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadGoc() {
        string pathh = "gamedata.txt";
        string path = System.IO.Path.Combine(Application.persistentDataPath, pathh);
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

}
