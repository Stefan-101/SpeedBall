using Unity.Netcode;
using UnityEngine;

public class BallNetwork : NetworkBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!IsServer)
        {
            rb.isKinematic = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (IsServer)
            Debug.Log("Ball hit by: " + collision.gameObject.name);
    }

}
