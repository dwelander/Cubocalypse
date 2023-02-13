using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public float dashSpeed = 20f;
    public float dashDistance = 1f;
    public Camera cam;
    public bool isDashing;
    public float dashCooldown = 2f;

    private Vector2 movement;
    private Vector2 mousePos;
    private Vector2 dashVector;
    private bool canDash = true;

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0)) {
            if (canDash) {
                mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
                dashVector = mousePos - rb.position;
                float temp = Mathf.Max(Mathf.Abs(dashVector.x), Mathf.Abs(dashVector.y));
                dashVector /= temp;
                StartCoroutine(Dash());
            }
        }
    }

    private void FixedUpdate() {
        if (!isDashing) {
            rb.velocity = movement * moveSpeed;
        }
    }

    IEnumerator Dash() {
        isDashing = true;
        canDash = false;
        rb.velocity = dashVector * dashSpeed;
        yield return new WaitForSeconds(dashDistance / 2);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        yield return null;
    }
}

