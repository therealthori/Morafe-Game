using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Forward & Lane Movement")]
    public float forwardSpeed     = 5f;     // Z axis (towards the horizon)
    public float horizontalSpeed  = 3f;     // Lane‑change speed
    public float leftLimit        = -5.5f;
    public float rightLimit       =  5.5f;

    [Header("Jump")]
    public float jumpHeight       = 2f;     // How high, in metres, a jump should reach
    public float gravity          = -20f;   // Use a large negative for snappier arcs

    CharacterController controller;
    [SerializeField] Animator animator;
    float yVelocity;                        // Current vertical speed (m/s)

    void Awake() => controller = GetComponent<CharacterController>();

    void Update()
    {
        // ---------- Horizontal input (A / D or arrows) ----------
        float xDir = 0f;
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) &&
            transform.position.x > leftLimit)
            xDir = -horizontalSpeed;
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
                 transform.position.x < rightLimit)
            xDir =  horizontalSpeed;

        // ---------- Jump & gravity ----------
        if (controller.isGrounded)
        {
            // Small downward bias keeps us snapped to ground on slopes
            if (yVelocity < 0f) yVelocity = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // v = sqrt(2 * g * h)   (g is negative)
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                animator.SetTrigger("Jump");
            }
            animator.SetBool("isGrounded", controller.isGrounded);
        }

        yVelocity += gravity * Time.deltaTime;

        // ---------- Compose movement vector ----------
        Vector3 move = new Vector3(xDir, yVelocity, forwardSpeed);

        // CharacterController.Move expects speed in m/s → multiply by Δt
        controller.Move(move * Time.deltaTime);

        // ---------- Hard‑clamp X so we never drift outside lanes ----------
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);
        transform.position = pos;
    }
}