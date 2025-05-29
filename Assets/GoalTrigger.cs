using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    
    public GameObject gameManager;
    Rigidbody2D rb;

    void Start()
    {

    }

    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LeftGoal"))
        {
            gameManager.GetComponent<GameManager>().scoreRight += 1;
            Debug.Log("Right Player Scored!");
        }
        else if (collision.CompareTag("RightGoal"))
        {
            gameManager.GetComponent<GameManager>().scoreLeft += 1;
            Debug.Log("Left Player Scored!");
        }
        transform.position = new Vector3(0, 0, 0);
        rb.linearVelocityX = 0;
        rb.linearVelocityY = 0;
        rb.angularVelocity = 0;
        gameManager.GetComponent<GameManager>().ResetGame();
    }
}
