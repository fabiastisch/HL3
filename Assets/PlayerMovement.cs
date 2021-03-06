using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;

    public float speed = 12f;

    public Vector3 velocity;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    public float jumpHeight = 3f;

    public int jumps = 2;

    private int currentJumps;

    public float sprintSpeedMuliplier = 1.4f;

    public float dashTime = 0.2f;
    public float dashCoolDown = 1f;
    public float dashSpeedMultiplier = 10f;
    private bool isDashOnCooldown = false;

    private Vector3 spawnPosition;
    private void Start() {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
            currentJumps = jumps;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && currentJumps > 0) {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            Debug.Log("Dash");
            if (!isDashOnCooldown) {
                Dash();
            }
        }

        Vector3 move = transform.right * x + transform.forward * z;


        velocity.y += gravity * Time.deltaTime; // Movement Y Direction (Jump & Gravity)
        if (this.IsSprinting()) {
            controller.Move(move * (speed * sprintSpeedMuliplier * Time.deltaTime) + velocity * Time.deltaTime);
        }
        else {
            controller.Move(move * (speed * Time.deltaTime) + velocity * Time.deltaTime);
        }

    }

    private IEnumerator DashCoroutine() {
        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + dashTime) {
            controller.Move(transform.forward * (sprintSpeedMuliplier * speed * dashSpeedMultiplier * Time.deltaTime));
            yield return null; // this will make Unity stop here and continue next frame
        }
    }

    private bool IsSprinting() {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private void Dash() {
        isDashOnCooldown = true;
        GameSettings.Instance.cooldownManager.StartDashing(dashCoolDown);
        Invoke(nameof(ResetDash), dashCoolDown);
        StartCoroutine(DashCoroutine());
    }

    private void ResetDash() {
        isDashOnCooldown = false;
    }

    public void Jump() {
        currentJumps--;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public Vector3 GetCurrentPlayerVelocity() {
        return controller.velocity;
    }

    public int GetCurrentJumps() {
        return currentJumps;
    }

    public void Die() {
        Debug.Log("Die");
        transform.position = spawnPosition;
    }
}