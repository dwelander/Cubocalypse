using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public float dashSpeed = 20f;
    public float dashDistance = 0.5f;
    public Camera cam;
    public bool isDashing;
    public float dashCooldown = 2f;
    public Bomb bombPrefab;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private Vector2 movement;
    private Vector2 mousePos;
    private Vector2 dashVector;
    private bool canDash = true;
    private Player player;
    private TrailRenderer trail;

    private void Awake() {
        player = GetComponent<Player>();
        trail = GetComponent<TrailRenderer>();
    }

    void Update() {
        transform.rotation = Quaternion.identity;
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }

        if (movement.x > 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "YWall") {
            if (isDashing) {
                StopCoroutine(Dash());
                dashVector *= new Vector2(1, -1);
                StartCoroutine(Dash());
            }
        } else if (collision.gameObject.tag == "XWall") {
            if (isDashing) {
                StopCoroutine(Dash());
                dashVector *= new Vector2(-1, 1);
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
        trail.emitting = true;
        canDash = false;
        rb.velocity = dashVector * dashSpeed;
        yield return new WaitForSeconds(dashDistance / 2);
        if (player.lives <= 8) {
            Instantiate(bombPrefab, rb.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(dashDistance / 2);
        isDashing = false;
        trail.emitting = false;
        StartCoroutine(UIManager.Instance.DashUI(dashCooldown));
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        yield return null;
    }
}

