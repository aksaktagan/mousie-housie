using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Transform cam1;
    public Transform cam2;
    public Transform cam3;

    CameraSwitch1 cs1;
    CameraSwitch2 cs2;
    CameraSwitch3 cs3;

    GameObject cat;

    CharacterController controller;
    float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    Vector2 movement;
    public float moveSpeed;

    private bool isWalking;
    public bool Grounded = true;

    public Vector3 spherePosition;
    public float GroundedOffset = 1.35f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;

    // timeout and jump deltatime
    private float _fallTimeoutDelta;
    private float _jumpTimeoutDelta;

    public float FallTimeout = 0.15f;

    // player
    public float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    public float JumpTimeout = 0.50f;

    public float Gravity = -15.0f;

    public float JumpHeight = 1.2f;


    // Draw the ground detecting sphere
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, GroundedRadius);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        cat = GetComponent<GameObject>();

        cs1 = GameObject.FindFirstObjectByType<CameraSwitch1>();
        cs2 = GameObject.FindFirstObjectByType<CameraSwitch2>();
        cs3 = GameObject.FindFirstObjectByType<CameraSwitch3>();
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }

    public void Update()
    {
        GroundedCheck();    // is character on the ground
        JumpAndGravity();   // calculate speed to due gravity

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 dir = new Vector3(movement.x, 0f, movement.y).normalized;

        isWalking = dir != Vector3.zero;

        float magnitude = dir.magnitude;

        //controller.SimpleMove(dir * magnitude * moveSpeed+ Vector3.up * _verticalVelocity * Time.deltaTime);

        if (magnitude >= 0.1f)
        {
            if (cs1.isCamOneActive())
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam1.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                controller.transform.rotation = transform.rotation;

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;       // gravity

                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime + Vector3.up * _verticalVelocity * Time.deltaTime);

            }
            else if (cs2.isCamTwoActive())
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam2.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                controller.transform.rotation = transform.rotation;

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;       // gravity

                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime + Vector3.up * _verticalVelocity * Time.deltaTime);
            }
            else if (cs3.isCamThreeActive())
            {
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam3.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                controller.transform.rotation = transform.rotation;

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;       // gravity

                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime + Vector3.up * _verticalVelocity * Time.deltaTime);

            }
        }
        else // no key pressed, but apply gravity
        {
            controller.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Jump
            if (_jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

            }
        }
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }


        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("GameLost");
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
