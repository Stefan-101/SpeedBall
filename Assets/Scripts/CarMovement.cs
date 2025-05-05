using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Codul original pentru mișcarea mașinii cu modificările pentru joc fotbal
public class CarMovement : MonoBehaviour
{
    private float horizontal;
    private float storedHorizontalVelocity = 0f;
    private float acceleration = 50f; // Acceleration rate
    private float decelerationGrounded = 8f; // Deceleration rate when grounded
    private float decelerationAirborne = 3f; // Deceleration rate when airborne
    private float maxSpeed = 8f; // Maximum horizontal speed
    private float jumpingPower = 6.5f;
    private bool isFacingRight = true;
    private int flipsLeft = 0;
    private bool isFlipping = false;
    private bool canFlip = false;
    private float jumpTimer;
    private float jumpTimeWindow = 2.5f; // time window to allow flipping jumping
    private float flipTorquePower = 6.5f; // torque power for flipping
    private float airboneTorquePower = 1.5f;
    private float boostPower = 1.2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            // Creează un groundCheck dacă nu există
            GameObject checkObj = new GameObject("GroundCheck");
            checkObj.transform.parent = transform;
            checkObj.transform.localPosition = new Vector3(0, -1f, 0);
            groundCheck = checkObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        // first jump
        if (Input.GetMouseButtonDown(1) && isGrounded())
        {
            rb.AddForce(new Vector2(0f, jumpingPower), ForceMode2D.Impulse);
            canFlip = true;
            jumpTimer = Time.time;
        }

        // only after leaving the ground, allow the user to do a flip
        if (Input.GetMouseButtonUp(1) && canFlip)
        {
            canFlip = false;
            flipsLeft = 1;
        }

        // flips expire after some time
        if (Time.time - jumpTimer > jumpTimeWindow)
        {
            flipsLeft = 0;
        }

        // handle flip
        if (flipsLeft > 0 && Input.GetMouseButtonDown(1))
        {
            flipsLeft -= 1;
            FlipCar();
        }

        if (Input.GetMouseButton(0))
        {
            ApplyBoost();
        }

        // differentiate between being grounded and airbornea
        if (!isGrounded())
        {
            // Rotate the car when airborne
            if (!isFlipping)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    rb.angularVelocity = airboneTorquePower * 100f; // Rotate counterclockwise
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    rb.angularVelocity = -airboneTorquePower * 100f; // Rotate clockwise
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    rb.angularVelocity = 0f; // Stop rotation when no key is pressed
                }
            }
        }
    }

    private void ApplyBoost()
    {
        if (isFlipping)
        {
            return; // Don't apply boost while flipping
        }

        // Apply a constant force to the back of the car
        Vector2 boostForce = -transform.right * boostPower;
        rb.AddForce(boostForce, ForceMode2D.Force);
    }

    private void FlipCar()
    {
        isFlipping = true;

        float torqueDirection = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            torqueDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            torqueDirection = -1f;
        }

        // apply linear force in the flip direction
        Vector2 forceDirection = Quaternion.Euler(0, 0, rb.rotation) * new Vector2(-torqueDirection * 7.5f, 0f);
        rb.AddForce(forceDirection, ForceMode2D.Impulse);

        // apply the torque
        rb.AddTorque(torqueDirection * flipTorquePower, ForceMode2D.Impulse);

        if (torqueDirection == 0f)
        {
            Vector2 jumpForce = transform.up * (jumpingPower * 0.7f);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);

            isFlipping = false;
        }

        Invoke(nameof(ResetFlippingStatus), 1f);
    }

    private void ResetFlippingStatus()
    {
        isFlipping = false;
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.9045f, groundLayer);
    }

    private void FixedUpdate()
    {
        storedHorizontalVelocity = rb.linearVelocity.x;

        if (isGrounded())
        {
            // Accelerate when movement keys are pressed
            if (horizontal != 0)
            {
                storedHorizontalVelocity += horizontal * acceleration * Time.fixedDeltaTime;
            }
            else
            {
                // Decelerate when no movement keys are pressed
                storedHorizontalVelocity = Mathf.MoveTowards(storedHorizontalVelocity, 0f, decelerationGrounded * Time.fixedDeltaTime);
            }
        }
        else
        {
            // Decelerate more slowly when airborne
            storedHorizontalVelocity = Mathf.MoveTowards(storedHorizontalVelocity, 0f, decelerationAirborne * Time.fixedDeltaTime);
        }

        // Clamp the horizontal velocity to the maximum speed
        storedHorizontalVelocity = Mathf.Clamp(storedHorizontalVelocity, -maxSpeed, maxSpeed);

        // Apply the horizontal velocity to the Rigidbody2D
        rb.linearVelocity = new Vector2(storedHorizontalVelocity, rb.linearVelocity.y);
    }


    private void flipFacing()
    {
        if (isGrounded() && (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}

// Cod nou pentru terenul de fotbal
/*
public class SoccerField2D : MonoBehaviour
{
    [Header("Field Settings")]
    public float fieldWidth = 40f;
    public float fieldHeight = 20f;
    public Color fieldColor = new Color(0.2f, 0.7f, 0.2f); // Verde pentru teren

    [Header("Wall Settings")]
    public float wallThickness = 1f;
    public Color wallColor = new Color(0.3f, 0.3f, 0.3f); // Gri închis pentru pereți

    [Header("Goal Settings")]
    public float goalWidth = 7f;
    public float goalHeight = 7f;
    public float goalDepth = 2f;
    public Color goalColor = new Color(0.8f, 0.8f, 0.8f); // Gri deschis pentru porți

    [Header("Ball Settings")]
    public float ballRadius = 1f;
    public float ballBounciness = 0.7f;
    public Color ballColor = new Color(1f, 0.5f, 0f); // Portocaliu pentru minge

    private GameObject ball;

    void Start()
    {
        CreateField();
        CreateWalls();
        CreateGoals();
        CreateBall();
        SetupScoreUI();
    }

    void CreateField()
    {
        GameObject field = new GameObject("Field");
        field.transform.parent = transform;

        SpriteRenderer fieldRenderer = field.AddComponent<SpriteRenderer>();
        fieldRenderer.color = fieldColor;

        Texture2D fieldTexture = new Texture2D(1, 1);
        fieldTexture.SetPixel(0, 0, Color.white);
        fieldTexture.Apply();

        Sprite fieldSprite = Sprite.Create(fieldTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        fieldRenderer.sprite = fieldSprite;

        field.transform.localScale = new Vector3(fieldWidth, fieldHeight, 1f);

        BoxCollider2D fieldCollider = field.AddComponent<BoxCollider2D>();
        fieldCollider.isTrigger = false;
    }

    void CreateWalls()
    {
        // Pereții de sus și jos
        CreateWall(new Vector2(0, fieldHeight / 2 + wallThickness / 2), new Vector2(fieldWidth, wallThickness), "TopWall");
        CreateWall(new Vector2(0, -fieldHeight / 2 - wallThickness / 2), new Vector2(fieldWidth, wallThickness), "BottomWall");

        // Pereții laterali (cu găuri pentru porți)
        float topSegmentHeight = (fieldHeight - goalHeight) / 2;

        // Perete stânga (două segmente cu un gol pentru poartă)
        CreateWall(new Vector2(-fieldWidth / 2 - wallThickness / 2, topSegmentHeight / 2 + goalHeight / 2),
                  new Vector2(wallThickness, topSegmentHeight), "LeftWallTop");
        CreateWall(new Vector2(-fieldWidth / 2 - wallThickness / 2, -topSegmentHeight / 2 - goalHeight / 2),
                  new Vector2(wallThickness, topSegmentHeight), "LeftWallBottom");

        // Perete dreapta (două segmente cu un gol pentru poartă)
        CreateWall(new Vector2(fieldWidth / 2 + wallThickness / 2, topSegmentHeight / 2 + goalHeight / 2),
                  new Vector2(wallThickness, topSegmentHeight), "RightWallTop");
        CreateWall(new Vector2(fieldWidth / 2 + wallThickness / 2, -topSegmentHeight / 2 - goalHeight / 2),
                  new Vector2(wallThickness, topSegmentHeight), "RightWallBottom");
    }

    void CreateWall(Vector2 position, Vector2 size, string name)
    {
        GameObject wall = new GameObject(name);
        wall.transform.parent = transform;
        wall.transform.position = position;

        SpriteRenderer wallRenderer = wall.AddComponent<SpriteRenderer>();
        wallRenderer.color = wallColor;

        Texture2D wallTexture = new Texture2D(1, 1);
        wallTexture.SetPixel(0, 0, Color.white);
        wallTexture.Apply();

        Sprite wallSprite = Sprite.Create(wallTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        wallRenderer.sprite = wallSprite;

        wall.transform.localScale = new Vector3(size.x, size.y, 1f);

        BoxCollider2D wallCollider = wall.AddComponent<BoxCollider2D>();
        wallCollider.isTrigger = false;

        PhysicsMaterial2D physicsMat = new PhysicsMaterial2D("WallPhysics");
        physicsMat.bounciness = 0.7f;
        physicsMat.friction = 0.2f;
        wallCollider.sharedMaterial = physicsMat;
    }

    void CreateGoals()
    {
        // Porțile pe ambele părți ale terenului
        CreateGoal(new Vector2(-fieldWidth / 2 - goalDepth / 2, 0), true, "LeftGoal");
        CreateGoal(new Vector2(fieldWidth / 2 + goalDepth / 2, 0), false, "RightGoal");
    }

    void CreateGoal(Vector2 position, bool isLeftGoal, string name)
    {
        GameObject goalParent = new GameObject(name);
        goalParent.transform.parent = transform;
        goalParent.transform.position = position;

        // Partea din spate a porții
        GameObject backWall = new GameObject("BackWall");
        backWall.transform.parent = goalParent.transform;
        backWall.transform.localPosition = Vector2.zero;

        SpriteRenderer backRenderer = backWall.AddComponent<SpriteRenderer>();
        backRenderer.color = goalColor;

        Texture2D backTexture = new Texture2D(1, 1);
        backTexture.SetPixel(0, 0, Color.white);
        backTexture.Apply();

        Sprite backSprite = Sprite.Create(backTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        backRenderer.sprite = backSprite;

        backWall.transform.localScale = new Vector3(wallThickness, goalHeight, 1f);

        BoxCollider2D backCollider = backWall.AddComponent<BoxCollider2D>();
        backCollider.isTrigger = false;

        // Partea de sus a porții
        GameObject topWall = new GameObject("TopWall");
        topWall.transform.parent = goalParent.transform;
        topWall.transform.localPosition = new Vector2(isLeftGoal ? goalDepth / 2 : -goalDepth / 2, goalHeight / 2 + wallThickness / 2);

        SpriteRenderer topRenderer = topWall.AddComponent<SpriteRenderer>();
        topRenderer.color = goalColor;

        Texture2D topTexture = new Texture2D(1, 1);
        topTexture.SetPixel(0, 0, Color.white);
        topTexture.Apply();

        Sprite topSprite = Sprite.Create(topTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        topRenderer.sprite = topSprite;

        topWall.transform.localScale = new Vector3(goalDepth, wallThickness, 1f);

        BoxCollider2D topCollider = topWall.AddComponent<BoxCollider2D>();
        topCollider.isTrigger = false;

        // Zona de trigger pentru gol
        GameObject goalTrigger = new GameObject("GoalTrigger");
        goalTrigger.transform.parent = goalParent.transform;
        goalTrigger.transform.localPosition = new Vector2(isLeftGoal ? goalDepth / 2 : -goalDepth / 2, 0);

        BoxCollider2D triggerCollider = goalTrigger.AddComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector2(goalDepth - 0.1f, goalHeight - 0.1f);

        GoalTrigger2D goalScript = goalTrigger.AddComponent<GoalTrigger2D>();
        goalScript.isLeftGoal = isLeftGoal;
    }

    void CreateBall()
    {
        ball = new GameObject("Ball");
        ball.transform.position = new Vector3(0, 0, 0);
        ball.tag = "Ball"; // Adaugă și un tag "Ball" în Unity

        SpriteRenderer ballRenderer = ball.AddComponent<SpriteRenderer>();
        ballRenderer.color = ballColor;

        Texture2D ballTexture = CreateCircleTexture(128, ballColor);

        Sprite ballSprite = Sprite.Create(ballTexture, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f));
        ballRenderer.sprite = ballSprite;

        ball.transform.localScale = new Vector3(ballRadius * 2, ballRadius * 2, 1f);

        CircleCollider2D ballCollider = ball.AddComponent<CircleCollider2D>();
        ballCollider.radius = 0.5f;

        Rigidbody2D ballRb = ball.AddComponent<Rigidbody2D>();
        ballRb.mass = 1f;
        ballRb.linearDamping = 0.2f;
        ballRb.angularDamping = 0.1f;
        // Folosim gravitație pentru a face jocul mai realist
        ballRb.gravityScale = 1f;

        PhysicsMaterial2D ballPhysics = new PhysicsMaterial2D("BallPhysics");
        ballPhysics.bounciness = ballBounciness;
        ballPhysics.friction = 0.2f;
        ballCollider.sharedMaterial = ballPhysics;
    }

    void SetupScoreUI()
    {
        // Verifică dacă există deja un GameManager
        if (FindObjectOfType<GameManager>() == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
        }
    }

    Texture2D CreateCircleTexture(int size, Color color)
    {
        Texture2D texture = new Texture2D(size, size);

        Color[] colors = new Color[size * size];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.clear;
        }
        texture.SetPixels(colors);

        float center = size / 2f;
        float radiusSquared = center * center;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dx = center - x;
                float dy = center - y;
                if (dx * dx + dy * dy < radiusSquared)
                {
                    texture.SetPixel(x, y, color);
                }
            }
        }

        texture.Apply();
        return texture;
    }
}

// Script pentru detectarea golurilor
public class GoalTrigger2D : MonoBehaviour
{
    public bool isLeftGoal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // În funcție de poartă, adaugă scorul echipei corespunzătoare
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                if (isLeftGoal)
                {
                    gameManager.ScoreRight(); // Echipa din dreapta înscrie în poarta din stânga
                }
                else
                {
                    gameManager.ScoreLeft(); // Echipa din stânga înscrie în poarta din dreapta
                }
            }

            StartCoroutine(ResetBallPosition(other.gameObject));
        }
    }

    private IEnumerator ResetBallPosition(GameObject ball)
    {
        yield return new WaitForSeconds(1.5f);

        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            ballRb.linearVelocity = Vector2.zero;
            ballRb.angularVelocity = 0f;
            ball.transform.position = new Vector2(0f, 3f);
        }
    }
}

// Manager pentru scor și starea jocului
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int scoreLeft = 0;
    public int scoreRight = 0;

    private Text scoreTextLeft;
    private Text scoreTextRight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CreateScoreDisplay();
    }

    private void CreateScoreDisplay()
    {
        // Crează un canvas pentru UI dacă nu există
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        // Crează afișajul pentru scorul stânga
        GameObject leftScoreObj = new GameObject("LeftScore");
        leftScoreObj.transform.SetParent(canvas.transform, false);

        scoreTextLeft = leftScoreObj.AddComponent<Text>();
        scoreTextLeft.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        scoreTextLeft.fontSize = 36;
        scoreTextLeft.alignment = TextAnchor.MiddleCenter;
        scoreTextLeft.color = Color.blue;
        scoreTextLeft.text = "0";

        RectTransform leftRT = leftScoreObj.GetComponent<RectTransform>();
        leftRT.anchoredPosition = new Vector2(-100, 180);
        leftRT.sizeDelta = new Vector2(100, 50);

        // Crează afișajul pentru scorul dreapta
        GameObject rightScoreObj = new GameObject("RightScore");
        rightScoreObj.transform.SetParent(canvas.transform, false);

        scoreTextRight = rightScoreObj.AddComponent<Text>();
        scoreTextRight.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        scoreTextRight.fontSize = 36;
        scoreTextRight.alignment = TextAnchor.MiddleCenter;
        scoreTextRight.color = Color.red;
        scoreTextRight.text = "0";

        RectTransform rightRT = rightScoreObj.GetComponent<RectTransform>();
        rightRT.anchoredPosition = new Vector2(100, 180);
        rightRT.sizeDelta = new Vector2(100, 50);
    }

    public void ScoreLeft()
    {
        scoreLeft++;
        UpdateScoreDisplay();
    }

    public void ScoreRight()
    {
        scoreRight++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        // Actualizează textele de scor
        if (scoreTextLeft != null)
            scoreTextLeft.text = scoreLeft.ToString();

        if (scoreTextRight != null)
            scoreTextRight.text = scoreRight.ToString();
    }
  }
*/