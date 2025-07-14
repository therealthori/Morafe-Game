using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Forward & Lane Movement")]
    public float playerSpeed      =  2f;
    public float horizontalSpeed  =  3f;
    public float rightLimit       =  5.5f;
    public float leftLimit        = -5.5f;

    [Header("Jump")]
    public float jumpForce        =  6f;          // Upward impulse
    public float groundCheckDist  =  0.15f;       // Ray length below feet
    public LayerMask groundMask;                  // Assign your “Ground” layer here

    Rigidbody rb;
    bool      isGrounded;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        // --- Endless forward motion ----------------------------------------
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime,
                            Space.World);

        // --- Lane movement (A / D or Arrow keys) ---------------------------
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) &&
            transform.position.x > leftLimit)
        {
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime,
                                Space.World);
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
            transform.position.x < rightLimit)
        {
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime,
                                Space.World);
        }

        // --- Ground check ---------------------------------------------------
        isGrounded = Physics.Raycast(transform.position,
                                     Vector3.down,
                                     groundCheckDist + 0.01f,
                                     groundMask);

        // --- Jump input -----------------------------------------------------
        if (isGrounded && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Reset any downward velocity before jumping
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}