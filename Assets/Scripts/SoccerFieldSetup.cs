using UnityEngine;
using UnityEngine.UI;

public class SoccerFieldSetup : MonoBehaviour
{
    // Referințe la prefab-uri sau componente
    public GameObject goalPrefab; // Prefab pentru poartă (dacă nu folosiți unul existent)
    public Transform leftGoalPosition;
    public Transform rightGoalPosition;

    // Referințe pentru UI
    public Text leftScoreText;
    public Text rightScoreText;

    // Minge și sistem de scor
    public GameObject ball;
    public GoalSystem goalSystem;

    // Dimensiunile porților
    public float goalWidth = 3f;
    public float goalHeight = 5f;

    void Start()
    {
        // Inițializăm sistemul de scor dacă nu există deja
        if (goalSystem == null)
        {
            goalSystem = FindObjectOfType<GoalSystem>();
            if (goalSystem == null)
            {
                GameObject systemObj = new GameObject("GoalSystem");
                goalSystem = systemObj.AddComponent<GoalSystem>();
            }
        }

        // Configurăm sistemul de scor
        SetupGoalSystem();

        // Creăm porțile dacă nu există deja
        SetupGoals();
    }

    void SetupGoalSystem()
    {
        if (goalSystem != null)
        {
            goalSystem.leftScoreText = leftScoreText;
            goalSystem.rightScoreText = rightScoreText;
            goalSystem.ball = ball;
        }
    }

    void SetupGoals()
    {
        // Creăm poarta din stânga dacă nu există
        if (leftGoalPosition == null)
        {
            GameObject leftGoalObj = new GameObject("LeftGoal");
            leftGoalPosition = leftGoalObj.transform;
            leftGoalPosition.position = new Vector3(-10f, 0f, 0f); // Poziție implicită
        }

        // Creăm poarta din dreapta dacă nu există
        if (rightGoalPosition == null)
        {
            GameObject rightGoalObj = new GameObject("RightGoal");
            rightGoalPosition = rightGoalObj.transform;
            rightGoalPosition.position = new Vector3(10f, 0f, 0f); // Poziție implicită
        }

        // Configurăm trigger-ele pentru porți
        SetupGoalTrigger(leftGoalPosition, true);
        SetupGoalTrigger(rightGoalPosition, false);

        // Configurăm colider-ele pentru porți (opțional - dacă doriți ca porțile să aibă structură fizică)
        SetupGoalColliders(leftGoalPosition);
        SetupGoalColliders(rightGoalPosition);
    }

    void SetupGoalTrigger(Transform goalPosition, bool isLeftGoal)
    {
        // Creăm un obiect copil pentru trigger
        GameObject triggerObj = new GameObject(isLeftGoal ? "LeftGoalTrigger" : "RightGoalTrigger");
        triggerObj.transform.parent = goalPosition;
        triggerObj.transform.localPosition = Vector3.zero;

        // Adăugăm un Box Collider 2D pentru detecția golurilor
        BoxCollider2D triggerCollider = triggerObj.AddComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector2(1f, goalHeight);

        // Adăugăm script-ul de trigger pentru gol
        GoalTrigger goalTrigger = triggerObj.AddComponent<GoalTrigger>();
        goalTrigger.goalSystem = goalSystem;
        goalTrigger.isLeftGoal = isLeftGoal;
        goalTrigger.ballTag = "Ball"; // Asigurați-vă că mingea are acest tag
    }

    void SetupGoalColliders(Transform goalPosition)
    {
        // Creăm structura fizică a porții (bara de sus și barele laterale)

        // Bara de sus
        GameObject topBar = new GameObject("TopBar");
        topBar.transform.parent = goalPosition;
        topBar.transform.localPosition = new Vector3(0, goalHeight / 2, 0);

        BoxCollider2D topCollider = topBar.AddComponent<BoxCollider2D>();
        topCollider.size = new Vector2(goalWidth, 0.2f);

        // Bara din stânga
        GameObject leftBar = new GameObject("LeftBar");
        leftBar.transform.parent = goalPosition;
        leftBar.transform.localPosition = new Vector3(-goalWidth / 2, 0, 0);

        BoxCollider2D leftCollider = leftBar.AddComponent<BoxCollider2D>();
        leftCollider.size = new Vector2(0.2f, goalHeight);

        // Bara din dreapta
        GameObject rightBar = new GameObject("RightBar");
        rightBar.transform.parent = goalPosition;
        rightBar.transform.localPosition = new Vector3(goalWidth / 2, 0, 0);

        BoxCollider2D rightCollider = rightBar.AddComponent<BoxCollider2D>();
        rightCollider.size = new Vector2(0.2f, goalHeight);

        // Adăugăm și Rigidbody2D pentru fiecare bară pentru a le face statice
        Rigidbody2D topRb = topBar.AddComponent<Rigidbody2D>();
        topRb.bodyType = RigidbodyType2D.Static;

        Rigidbody2D leftRb = leftBar.AddComponent<Rigidbody2D>();
        leftRb.bodyType = RigidbodyType2D.Static;

        Rigidbody2D rightRb = rightBar.AddComponent<Rigidbody2D>();
        rightRb.bodyType = RigidbodyType2D.Static;
    }
}