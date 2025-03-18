using UnityEngine;

public class PlayerCharacter : Character
{
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
