using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallSync2D : MonoBehaviourPun, IPunObservable
{
    private Rigidbody2D rb;

    private Vector2 networkPosition;
    private Vector2 networkVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            return;
        }

        rb.linearVelocity = networkVelocity;
        rb.MovePosition(Vector2.Lerp(rb.position, networkPosition, Time.fixedDeltaTime * 10));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.linearVelocity);
        }
        else
        {
            networkPosition = (Vector2)stream.ReceiveNext();
            networkVelocity = (Vector2)stream.ReceiveNext();
        }
    }
}
