using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeedMultiplier = 1.5f;
    
    private Rigidbody rb;
    private bool isRunning;

    private Transform cameraTransform;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on PlayerMovement!");
            enabled = false;
        }

        if (Camera.main) 
            cameraTransform = Camera.main.transform;
        
        if (cameraTransform == null)
        {
            Debug.LogError("Main Camera not found!");
        }
    }

    public void Move(Vector3 direction)
    {
        float currentSpeed = moveSpeed;
        if (isRunning)
        {
            currentSpeed *= runSpeedMultiplier;
        }
        
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;
        
        Vector3 moveDirection = cameraForward * direction.z + cameraRight * direction.x;
        moveDirection = moveDirection.normalized;
        
        float yVelocity = rb.linearVelocity.y;
        Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z) * currentSpeed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, yVelocity, horizontalVelocity.z);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
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
        float yVelocity = rb.linearVelocity.y;
        rb.linearVelocity = new Vector3(0, yVelocity, 0);
        
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