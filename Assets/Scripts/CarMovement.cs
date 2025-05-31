using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // car mass = 2
    // gravity scale = 1.5
    public int playerNumber = 1; // 1 for left player, 2 for right player, 3 online
    private float horizontal;
    private float storedHorizontalVelocity = 0f;
    private float acceleration = 80f; // Acceleration rate
    private float decelerationGrounded = 8f; // Deceleration rate when grounded
    private float decelerationAirborne = 3f; // Deceleration rate when airborne
    private float maxSpeed = 16f; // Maximum horizontal speed
    private float jumpingPower = 35f;
    private bool isFacingRight = true;
    private int flipsLeft = 0;
    private bool isFlipping = false;
    private bool canFlip = false;
    private float jumpTimer;
    private float jumpTimeWindow = 2.5f; // time window to allow flipping jumping
    private float flipTorquePower = 35f; // torque power for flipping
    private float airboneTorquePower = 3f;
    private float boostPower = 4.5f;
    private bool rotatingClockwise = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearDamping = 0.5f;  // helps with stopping naturally when not accelerating
        rb.angularDamping = 1.1f; // helps stabilize car rotation

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
        if (flipsLeft > 0 && Input.GetMouseButtonDown(1) && !isGrounded())
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
            // TODO apply the torque as an acceleration maybe?
            // Rotate the car when airborne
            if (!isFlipping)
            {
                float angularVelocity = airboneTorquePower * 100f;
                if (Input.GetKey(KeyCode.A))
                {
                    rb.angularVelocity = angularVelocity; // Rotate counterclockwise
                }
                else if (Input.GetKey(KeyCode.D) )
                {
                    rb.angularVelocity = -angularVelocity; // Rotate clockwise
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    rb.angularVelocity = 0f;
                }
            }
        }

        //flipFacing();
    }

    private void ApplyBoost()
    {
        if (isFlipping)
        {
            return; // Don't apply boost while flipping
        }

        // Apply a constant force to the back of the car
        Vector2 boostForce = -transform.right * boostPower;     // TODO check orientation of the car ?
        boostForce = isGrounded() ? boostForce * 1.5f : boostForce;
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

        // decrease jump height when flipping
        rb.AddForce(new Vector2(0f, -jumpingPower * 0.25f), ForceMode2D.Impulse);

        // apply linear force in the flip direction
        float angleOffset = 5f; // degrees
        float totalRotation = rb.rotation + torqueDirection * angleOffset;
        Vector2 forceDirection = Quaternion.Euler(0, 0, totalRotation) * new Vector2(-torqueDirection * 25f, 0f);
        rb.AddForce(forceDirection, ForceMode2D.Impulse);

        // apply the torque
        rb.angularVelocity = 0f;
        rb.AddTorque(torqueDirection * flipTorquePower, ForceMode2D.Impulse);

        if (torqueDirection == 0f)
        {
            Vector2 jumpForce = transform.up * (jumpingPower * 0.4f);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);

            isFlipping = false;
        }

        Invoke(nameof(ResetFlippingStatus), 0.5f);

        // TODO fine tune for flipTorquePower
        // TODO add a force in the "selected" direction
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

        if (isGrounded() && CheckPlayerKeybinds(playerNumber))
        {
            // Accelerate when movement keys are pressed
            if (horizontal != 0 && (storedHorizontalVelocity < maxSpeed && storedHorizontalVelocity > -maxSpeed))
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

        // Apply the horizontal velocity to the Rigidbody2D
        rb.linearVelocity = new Vector2(storedHorizontalVelocity, rb.linearVelocity.y);
    }

    private bool CheckPlayerKeybinds(int numberPlayer)
    {
        if (numberPlayer == 2)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (numberPlayer == 1)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
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
