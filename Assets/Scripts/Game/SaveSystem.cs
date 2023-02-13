using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public static class SaveSystem
{
    public static void SaveHighScoreBoard(HighScoreBoardData highScoreBoardData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = getFilePath();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, highScoreBoardData);
        stream.Close();
    }

    public static HighScoreBoardData LoadHighScoreBoard()
    {
        string path = getFilePath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighScoreBoardData data = formatter.Deserialize(stream) as HighScoreBoardData;
            stream.Close();

            return data;
        }

        Debug.LogError("Save file not found in " + path);
        return null;
    }

    private static string getFilePath()
    {
        return Application.persistentDataPath + "/highScoreBoard.fun";
    }
}
