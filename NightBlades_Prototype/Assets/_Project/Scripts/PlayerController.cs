using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBCore.Refs;
using Cinemachine;
using UnityEngine.InputSystem;
using Utilities;

namespace NBProtoype
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float gravityMultiplier = 3f;

        //[Header("Personality Switch Settings")]
        //[SerializeField] float switchDuration = 0.5f;
        //[SerializeField] float switchCooldown = 0f;

        const float ZeroF = 0f;

        Transform mainCam;

        float currentSpeed;
        float velocity;
        float jumpVelocity;

        Vector3 movement;

        List<Timer> timers;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;

        //CountdownTimer switchTimer;
        //CountdownTimer switchCooldownTimer;


        // Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;
            // Invoke event when observed transform is teleported, adjusting freeLookVCam's position accordingly
            freeLookVCam.OnTargetObjectWarped(transform, transform.position - freeLookVCam.transform.position - Vector3.forward);

            rb.freezeRotation = true;

            SetupTimers();
            //SetupStateMachine();
        }

        void SetupTimers()
        {
            // Setup timers
            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);

            jumpTimer.OnTimerStart += () => jumpVelocity = jumpForce;
            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();

            //switchTimer = new CountdownTimer(switchDuration);
            //switchCooldownTimer = new CountdownTimer(switchCooldown);

            //dashTimer = new CountdownTimer(dashDuration);
            //dashCooldownTimer = new CountdownTimer(dashCooldown);

            //dashTimer.OnTimerStart += () => dashVelocity = dashForce;
            //dashTimer.OnTimerStop += () => {
            //dashVelocity = 1f;
            //dashCooldownTimer.Start();
            //};

            //attackTimer = new CountdownTimer(attackCooldown);


            timers = new(2) { jumpTimer, jumpCooldownTimer };
            //dashTimer, dashCooldownTimer, attackTimer switchTimer
        }

        void Start() => input.EnablePlayerActions();

        void OnEnable()
        {
            input.Jump += OnJump;
            //input.Switch -= OnSwitch;
            //input.Dash += OnDash;
            //input.Attack += OnAttack;
        }

        void OnDisable()
        {
            input.Jump -= OnJump;
            //input.Switch -= OnSwitch;
            //input.Dash -= OnDash;
            //input.Attack -= OnAttack;
        }
        void OnJump(bool performed)
        {
            if (performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpTimer.Start();
            }
            else if (!performed && jumpTimer.IsRunning)
            {
                jumpTimer.Stop();
            }
        }
        void OnSwitch(bool performed)
        {
            // switch
        }
        void Update()
        {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);
            
            //stateMachine.Update();
            HandleTimers();
            UpdateAnimator();
        }
        void FixedUpdate()
        {
            HandleJump();
            HandleMovement();
        }

        void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }
        void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        public void HandleJump()
        {
            // If not jumping and grounded, keep jump velocity at 0
            if (!jumpTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpVelocity = ZeroF;
                return;
            }

            if (!jumpTimer.IsRunning)
            {
                // Gravity takes over
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            // Apply velocity
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        public void HandleMovement()
        {
            // Rotate movement direction to match camera rotation
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;

            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);

                // Reset horizontal velocity for a snappy stop
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
            }
        }
        void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            // Move the player
            Vector3 velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        void HandleRotation(Vector3 adjustedDirection)
        {
            // Adjust rotation to match movement direction
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.LookAt(transform.position + adjustedDirection);
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}
