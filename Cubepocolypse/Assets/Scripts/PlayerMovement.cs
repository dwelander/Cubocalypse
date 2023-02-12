using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    Vector2 movement;
    PlayerShape shape = PlayerShape.CIRCLE;
    public Camera cam;
    float speedIncrease = 1;

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(1)) {
            Vector2 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            switch (shape) {
                case PlayerShape.CIRCLE:
                    movement = mousePos - movement;
                    speedIncrease = 5;
                    break;
                case PlayerShape.TRIANGLE:
                    break;
            }
        }
    }

    private void FixedUpdate() {
        rb.AddForce(movement * moveSpeed * speedIncrease);
        speedIncrease = 1;
    }
}

public enum PlayerShape { CIRCLE, TRIANGLE }
