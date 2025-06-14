using UnityEngine;
using Fusion;

public class BallTest : NetworkBehaviour
{
    private Rigidbody2D rb;

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Object.HasStateAuthority)
        {
            Debug.Log("server, apply force");
            rb.AddForce(new Vector2(200, 300));
        }
    }
}
