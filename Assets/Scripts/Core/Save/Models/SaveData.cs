using System;
using UnityEngine;

namespace Core.Save.Models
{
    [Serializable]
    public class SaveData
    {
        public PlayerSaveData Player;
        public GameStateSaveData GameState;

        public SaveData()
        {
            Player = new PlayerSaveData();
            GameState = new GameStateSaveData();
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
} 