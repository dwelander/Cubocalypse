using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public float dashSpeed = 20f;
    public float dashDistance = 2f;
    public Camera cam;
    public bool isDashing;

    private Vector2 movement;
    private Vector2 mousePos;
    private Vector2 dashVector;

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0)) {
            mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            dashVector = mousePos - rb.position;
            float temp = Mathf.Max(Mathf.Abs(dashVector.x), Mathf.Abs(dashVector.y));
            dashVector /= temp;
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate() {
        if (!isDashing) {
            rb.velocity = movement * moveSpeed;
        }
    }

    IEnumerator Dash() {
        isDashing = true;
        rb.velocity = dashVector * dashSpeed;
        yield return new WaitForSeconds(dashDistance);
        isDashing = false;
        yield return null;
    }
}

