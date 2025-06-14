using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
public class BallNetworkSync2D : MonoBehaviourPun, IPunObservable
{
    private Rigidbody2D rb;
    private Vector2 networkPosition;
    private Vector2 networkVelocity;

    private float lerpSpeed = 15f;
    private float velocityLerpSpeed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (!photonView.IsMine)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            rb.position = Vector2.Lerp(rb.position, networkPosition, Time.fixedDeltaTime * lerpSpeed);
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, networkVelocity, Time.fixedDeltaTime * velocityLerpSpeed);
        }
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
