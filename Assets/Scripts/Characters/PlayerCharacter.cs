using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : Character
{
    protected override void Die()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        animationController.PlayDeathAnimation();

        Invoke(nameof(PlayerDead), 3f); 
    }

    private void PlayerDead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOverScene"); 
    }
}
