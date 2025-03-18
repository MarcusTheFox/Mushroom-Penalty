using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour, IMovable
{
    public float moveSpeed = 3f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on EnemyMovement!");
            enabled = false;
        }
    }

    public void Move(Vector3 direction)
    {
        float yVelocity = rb.linearVelocity.y;
        Vector3 horizontalVelocity = new Vector3(direction.x, 0, direction.z) * moveSpeed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, yVelocity, horizontalVelocity.z);

        AnimationController anim = GetComponent<AnimationController>();
        if(anim != null)
        {
            anim.PlayMoveAnimation(true, false);
        }
    }

    public void Stop()
    {
        float yVelocity = rb.linearVelocity.y;
        rb.linearVelocity = new Vector3(0, yVelocity, 0);
        
        AnimationController anim = GetComponent<AnimationController>();
        if(anim != null)
        {
            anim.PlayMoveAnimation(false, false);
        }
    }
}