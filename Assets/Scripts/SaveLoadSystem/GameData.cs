using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable] 
public class GameData
{
    public float[] playerPosition = new float[3];
    public float playerHp;
    public List<EnemyData> enemies = new List<EnemyData>();
}
