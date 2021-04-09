using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

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

    public float dashTime = 0.2f;
    public float dashCoolDown = 1f;
    public float dashDistance = 20f;
    private bool isDashing = false;
    
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
            if (!isDashing) {
                Dash();
            }
        }

        Vector3 move = transform.right * x +  transform.forward * z;
        if (isDashing) {
            move *=  dashDistance;
        }
        else {
        }
        
        velocity.y += gravity * Time.deltaTime; // Movement Y Direction (Jump & Gravity)
        controller.Move(move * (speed * Time.deltaTime) + velocity * Time.deltaTime);
    }

    private void Dash() {
        isDashing = true;
        Invoke(nameof(ResetDash), dashCoolDown);
    }

    private void ResetDash() {
        isDashing = false;
    }

    public void Jump() {
        currentJumps--;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public Vector3 GetCurrentPlayerVelocity() {
        return controller.velocity;
    }
}