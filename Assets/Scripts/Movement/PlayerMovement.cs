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

        rb.velocity = direction * currentSpeed;

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
        rb.velocity = Vector3.zero;
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