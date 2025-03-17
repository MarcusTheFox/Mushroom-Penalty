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
        rb.velocity = direction * moveSpeed;

        AnimationController anim = GetComponent<AnimationController>();
        if(anim != null)
        {
            anim.PlayMoveAnimation(true, false);
        }
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
        AnimationController anim = GetComponent<AnimationController>();
        if(anim != null)
        {
            anim.PlayMoveAnimation(false, false);
        }
    }
}