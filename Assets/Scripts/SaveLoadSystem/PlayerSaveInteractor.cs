using UnityEngine;
using System.Collections.Generic;


public class PlayerSaveInteractor
{
    private readonly SaveRepository repository;

    public PlayerSaveInteractor(SaveRepository repository)
    {
        this.repository = repository;
    }

    public void SaveGame(Vector3 playerPos, float playerHp, List<Enemy> enemies)
    {
        var data = new GameData
        {
            playerPosition = new float[] { playerPos.x, playerPos.y, playerPos.z },
            playerHp = playerHp,
            enemies = new List<EnemyData>()
        };

        foreach (var mob in enemies)
        {
            data.enemies.Add(new EnemyData
            {
                position = new float[] { mob.transform.position.x, mob.transform.position.y, mob.transform.position.z },
                hp = mob.HealthSystem.CurrentHealth
            });
        }

        repository.Save(data);
    }

    public GameData LoadGame()
    {
        var data = repository.Load();
        if (data == null)
        {
            return null;
        }
        return data;
    }
}
