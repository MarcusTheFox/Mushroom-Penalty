using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Save.Models
{
    [Serializable]
    public class SaveData
    {
        public PlayerSaveData Player;
        public GameStateSaveData GameState;
        public List<EnemySaveData> Enemies;

        public SaveData()
        {
            Player = new PlayerSaveData();
            GameState = new GameStateSaveData();
            Enemies = new List<EnemySaveData>();
        }
    }

    [Serializable]
    public class PlayerSaveData
    {
        public float Health;
        public Vector3 Position;
        public Vector3 Rotation;
    }

    [Serializable]
    public class GameStateSaveData
    {
        public float GameTime;
        public string CurrentScene;
    }

    [Serializable]
    public class EnemySaveData
    {
        public string Type;
        public float Health;
        public Vector3 Position;
        public Vector3 Rotation;
    }
} 