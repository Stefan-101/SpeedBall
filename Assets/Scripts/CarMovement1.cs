//using UnityEngine;
//using Fusion;

//[RequireComponent(typeof(Rigidbody2D))]
//public class CarMovement1 : MonoBehaviour
//{
//    public PlayerInputConfig inputConfig;

//    [Networked] private float storedHorizontalVelocity { get; set; }
//    [Networked] private bool jumpPressed { get; set; }
//    [Networked] private bool boostHeld { get; set; }
//    [Networked] private TickTimer flipResetTimer { get; set; }
//    [Networked] private bool isFlipping { get; set; }


//    [Networked] private Vector2 NetworkedPosition { get; set; }
//    [Networked] private float NetworkedRotation { get; set; }
//    [Networked] private Vector2 NetworkedVelocity { get; set; }
//    [Networked] private float NetworkedAngularVelocity { get; set; }

//    private float acceleration = 80f;
//    private float decelerationGrounded = 8f;
//    private float decelerationAirborne = 3f;
//    private float maxSpeed = 16f;
//    private float jumpingPower = 35f;

//    private bool isFacingRight = true;
//    private int flipsLeft = 0;
//    private bool canFlip = false;

//    private float jumpTimer;
//    private float jumpTimeWindow = 2.5f;
//    private float flipTorquePower = 35f;
//    private float airboneTorquePower = 3f;
//    private float boostPower = 4.25f;

//    [SerializeField] private Rigidbody2D rb;
//    [SerializeField] private Transform groundCheck;
//    [SerializeField] private LayerMask groundLayer;

//    private Vector2 latestInput;

//    public override void Spawned()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        rb.linearDamping = 0.5f;
//        rb.angularDamping = 1.1f;

//        if (inputConfig != null && inputConfig.isLeftPlayer)
//        {
//            FlipFacing();
//        }

//        if (Object.HasStateAuthority)
//        {
//            NetworkedPosition = rb.position;
//            NetworkedRotation = rb.rotation;
//            NetworkedVelocity = rb.linearVelocity;
//            NetworkedAngularVelocity = rb.angularVelocity;
//        }
//    }

//    public override void FixedUpdateNetwork()
//    {
//        if (Object.HasInputAuthority)
//        {
//            if (GetInput(out NetworkInputData data))
//            {
//                //latestInput = data.horizontal;
//                jumpPressed = data.jump;
//                boostHeld = data.boost;
//            }

//            ApplyMovement();
//            ApplyJump();
//            ApplyBoostIfNeeded();
//            HandleFlip();
//            HandleAirTorque();

//            if (isFlipping && flipResetTimer.Expired(Runner))
//            {
//                isFlipping = false;
//            }

//            NetworkedPosition = rb.position;
//            NetworkedRotation = rb.rotation;
//            NetworkedVelocity = rb.linearVelocity;
//            NetworkedAngularVelocity = rb.angularVelocity;
//        }
//        else if (Object.HasStateAuthority)
//        {
//            rb.position = NetworkedPosition;
//            rb.rotation = NetworkedRotation;
//            rb.linearVelocity = NetworkedVelocity;
//            rb.angularVelocity = NetworkedAngularVelocity;
//        }
//    }

//    private void ApplyMovement()
//    {
//        if (IsGrounded())
//        {
//            if (latestInput.x != 0 && Mathf.Abs(storedHorizontalVelocity) < maxSpeed)
//            {
//                storedHorizontalVelocity += latestInput.x * acceleration * Runner.DeltaTime;
//            }
//            else
//            {
//                storedHorizontalVelocity = Mathf.MoveTowards(storedHorizontalVelocity, 0f, decelerationGrounded * Runner.DeltaTime);
//            }
//        }
//        else
//        {
//            storedHorizontalVelocity = Mathf.MoveTowards(storedHorizontalVelocity, 0f, decelerationAirborne * Runner.DeltaTime);
//        }

//        Vector2 newVelocity = rb.linearVelocity;
//        newVelocity.x = storedHorizontalVelocity;
//        rb.linearVelocity = newVelocity;
//    }

//    private void ApplyJump()
//    {
//        if (jumpPressed && IsGrounded())
//        {
//            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
//            canFlip = true;
//            jumpTimer = Runner.SimulationTime;
//            flipsLeft = 1;
//        }

//        if (Runner.SimulationTime - jumpTimer > jumpTimeWindow)
//        {
//            flipsLeft = 0;
//        }
//    }

//    private void ApplyBoostIfNeeded()
//    {
//        if (boostHeld && !isFlipping)
//        {
//            Vector2 boostForce = transform.right * boostPower;
//            boostForce = isFacingRight ? boostForce : -boostForce;
//            boostForce *= IsGrounded() ? 1.5f : 1f;

//            rb.AddForce(boostForce, ForceMode2D.Force);
//        }
//    }

//    private void HandleFlip()
//    {
//        if (flipsLeft > 0 && jumpPressed && !IsGrounded())
//        {
//            flipsLeft--;
//            FlipCar();
//        }
//    }

//    private void HandleAirTorque()
//    {
//        if (!IsGrounded() && !isFlipping)
//        {
//            float angularVelocity = airboneTorquePower * 100f;
//            if (latestInput.x < 0)
//            {
//                rb.angularVelocity = angularVelocity;
//            }
//            else if (latestInput.x > 0)
//            {
//                rb.angularVelocity = -angularVelocity;
//            }
//            else
//            {
//                rb.angularVelocity = 0f;
//            }
//        }
//    }

//    private void FlipCar()
//    {
//        isFlipping = true;
//        flipResetTimer = TickTimer.CreateFromSeconds(Runner, 0.5f);

//        float torqueDirection = 0f;
//        if (latestInput.x < 0)
//            torqueDirection = 1f;
//        else if (latestInput.x > 0)
//            torqueDirection = -1f;

//        rb.AddForce(Vector2.down * jumpingPower * 0.25f, ForceMode2D.Impulse);

//        float angleOffset = 5f;
//        float totalRotation = rb.rotation + torqueDirection * angleOffset;
//        Vector2 forceDir = Quaternion.Euler(0, 0, totalRotation) * new Vector2(-torqueDirection * 25f, 0f);
//        rb.AddForce(forceDir, ForceMode2D.Impulse);

//        rb.angularVelocity = 0f;
//        rb.AddTorque(torqueDirection * flipTorquePower, ForceMode2D.Impulse);

//        if (torqueDirection == 0f)
//        {
//            rb.AddForce(transform.up * (jumpingPower * 0.4f), ForceMode2D.Impulse);
//            isFlipping = false;
//            flipResetTimer = TickTimer.None;
//        }
//    }

//    private bool IsGrounded()
//    {
//        return Physics2D.OverlapCircle(groundCheck.position, 0.9f, groundLayer);
//    }

//    private void FlipFacing()
//    {
//        isFacingRight = !isFacingRight;
//        Vector3 scale = transform.localScale;
//        scale.x *= -1;
//        transform.localScale = scale;
//    }
//}
