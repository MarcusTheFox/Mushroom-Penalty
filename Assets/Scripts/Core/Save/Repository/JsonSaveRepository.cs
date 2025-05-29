using UnityEngine;
using Core.Save.Models;
using System.IO;

namespace Core.Save.Repository
{
    public class JsonSaveRepository : ISaveRepository
    {
        private const string SAVE_FILE_NAME = "save.json";
        private readonly string savePath;

        public JsonSaveRepository()
        {
            savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        }

        public void Save(SaveData saveData)
        {
            string jsonData = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(savePath, jsonData);
        }

        public SaveData Load()
        {
            if (!HasSave())
                return new SaveData();

            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(jsonData);
        }

        public bool HasSave()
        {
            return File.Exists(savePath);
        }
    }
} 