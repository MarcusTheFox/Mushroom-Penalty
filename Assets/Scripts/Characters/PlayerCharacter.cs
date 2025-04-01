using UnityEngine;

public class PlayerCharacter : Character
{
    protected override void Die()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        animationController.PlayDeathAnimation();
    }

    private void PlayerDead()
    {
        Destroy(gameObject);
    }
}
