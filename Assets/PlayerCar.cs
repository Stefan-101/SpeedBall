

using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(NetworkTransform))]
public class PlayerCar : NetworkBehaviour
{
    public float moveForce = 10f;
    public float jumpForce = 12f;
    public float boostForce = 15f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    public LayerMask groundLayer;

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;



    }

    private bool CheckGrounded()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.5f;
        float radius = 0.1f;
        return Physics2D.OverlapCircle(origin, radius, groundLayer) != null;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData input))
        {
            if (HasStateAuthority)
            {
                isGrounded = CheckGrounded();

                rb.linearVelocity = new Vector2(input.horizontal * moveForce, rb.linearVelocity.y);

                if (input.jump && isGrounded)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    isGrounded = false;
                }

                if (input.boost && Mathf.Abs(input.horizontal) > 0.1f)
                {
                    rb.AddForce(Vector2.right * Mathf.Sign(input.horizontal) * boostForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                
            }
        }
    }

    public override void Render()
    {
        base.Render();

        if (!HasStateAuthority && HasInputAuthority)
        {
            if (rb != null)
            {
                rb.position = transform.position;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
}



