using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody rb;
    private AnimationController animationController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on EnemyMovement!", this);
            enabled = false;
        }
        
        animationController = GetComponent<AnimationController>();
        if (animationController == null)
        {
            Debug.LogError("Animation controller not fount on Enemy", this);
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
        
        if(animationController != null)
        {
            animationController.PlayMoveAnimation(false, false);
        }
    }
}