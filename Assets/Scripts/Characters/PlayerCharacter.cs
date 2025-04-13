using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
