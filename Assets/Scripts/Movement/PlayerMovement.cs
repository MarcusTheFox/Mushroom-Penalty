using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;

    private Rigidbody rb;
    private bool isRunning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on PlayerMovement!");
            enabled = false;
        }
    }

    public void Move(Vector3 direction)
    {
        float currentSpeed = moveSpeed;
        if (isRunning)
        {
            currentSpeed *= runSpeedMultiplier;
        }
        
        float yVelocity = rb.velocity.y;
        Vector3 horizontalVelocity = new Vector3(direction.x, 0, direction.z) * currentSpeed;
        rb.velocity = new Vector3(horizontalVelocity.x, yVelocity, horizontalVelocity.z);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        AnimationController anim = GetComponent<AnimationController>();
        if (anim)
        {
            anim.PlayMoveAnimation(true, isRunning);
        }

    }

    public void Stop()
    {
        float yVelocity = rb.velocity.y;
        rb.velocity = new Vector3(0, yVelocity, 0);
        
        AnimationController anim = GetComponent<AnimationController>();
        if(anim)
        {
            anim.PlayMoveAnimation(false, false);
        }

    }
    public void SetRunning(bool running)
    {
        isRunning = running;
    }
}