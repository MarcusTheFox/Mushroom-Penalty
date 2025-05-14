using System.IO;
using UnityEngine;

public class SaveRepository
{
    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json); // Перезаписывает или создает файл
    }

    public GameData Load()
    {
        if (!File.Exists(SavePath))
            return null;
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<GameData>(json);
    }
}
