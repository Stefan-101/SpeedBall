using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalSystem : MonoBehaviour
{
    // Referințe la textele care afișează scorul
    public Text leftScoreText;
    public Text rightScoreText;

    // Referință la minge
    public GameObject ball;

    // Poziția inițială a mingii pentru resetare
    private Vector3 ballStartPosition;

    // Scoruri
    public int leftScore = 0;
    public int rightScore = 0;

    // Referințe la efecte sau animații (opțional)
    public ParticleSystem goalEffect;

    void Start()
    {
        // Salvăm poziția inițială a mingii
        if (ball != null)
        {
            ballStartPosition = ball.transform.position;
        }

        // Inițializăm afișajul scorului
        UpdateScoreDisplay();
    }

    // Metodă apelată când mingea intră în poarta din stânga
    public void GoalScoredLeft()
    {
        rightScore++;
        UpdateScoreDisplay();
        PlayGoalEffect();
        ResetBall();
        Debug.Log("Scor stânga: " + leftScore + ", Scor dreapta: " + rightScore);
    }

    // Metodă apelată când mingea intră în poarta din dreapta
    public void GoalScoredRight()
    {
        leftScore++;
        UpdateScoreDisplay();
        PlayGoalEffect();
        ResetBall();
        Debug.Log("Scor stânga: " + leftScore + ", Scor dreapta: " + rightScore);
    }

    // Actualizează afișajul scorului
    public void UpdateScoreDisplay()
    {
        if (leftScoreText != null)
            leftScoreText.text = leftScore.ToString();

        if (rightScoreText != null)
            rightScoreText.text = rightScore.ToString();
    }

    // Resetează mingea la poziția inițială
    [System.Obsolete]
    public void ResetBall()
    {
        if (ball != null)
        {
            // Oprim toate forțele aplicate mingii
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.velocity = Vector2.zero;
                ballRb.angularVelocity = 0f;
            }

            // Resetăm poziția mingii
            ball.transform.position = ballStartPosition;

            // Opțional: Adăugați o mică întârziere înainte de a permite din nou interacțiunea
            StartCoroutine(BallResetCooldown());
        }
    }

    // Coroutină pentru a adăuga un cooldown după resetarea mingii
    public IEnumerator BallResetCooldown()
    {
        // Dezactivăm temporar coliziunile mingii (opțional)
        if (ball != null)
        {
            Collider2D ballCollider = ball.GetComponent<Collider2D>();
            if (ballCollider != null)
                ballCollider.enabled = false;
        }

        // Așteptăm o secundă
        yield return new WaitForSeconds(1f);

        // Reactivăm coliziunile
        if (ball != null)
        {
            Collider2D ballCollider = ball.GetComponent<Collider2D>();
            if (ballCollider != null)
                ballCollider.enabled = true;
        }
    }

    // Efecte vizuale la marcarea unui gol
    public void PlayGoalEffect()
    {
        if (goalEffect != null)
            goalEffect.Play();

        // Opțional: puteți adăuga și efecte sonore aici
    }

}