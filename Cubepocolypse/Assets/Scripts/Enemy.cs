using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public Player player;
    public int health = 20;
    public int damage = 10;
    public float knockback = 10f;
    public float deathAnimTime = 2f;
    public Collider2D col;
    public bool onScreen;

    private Camera cam;
    private SpriteRenderer spriteRenderer;
    private bool isDead;
    private Vector2 moveVector;
    private ParticleSystem particle;
    private bool frozen;
    private Animator animator;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
        onScreen = false;
    }

    private void FixedUpdate() {
        if (!isDead && !frozen) {
            Vector2 movement = player.transform.position - transform.position;
            float temp = Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y));
            movement /= temp;
            rb.velocity = movement * moveSpeed;
        }

        if (frozen && !isDead) {
            rb.velocity = Vector2.zero;
        }
    }

    private void Update() {
        if (frozen || isDead) {
            animator.SetBool("isMoving", false);
        } else {
            animator.SetBool("isMoving", true);
        }

        Vector2 viewPos = cam.WorldToViewportPoint(transform.position);
        if (viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1) {
            onScreen = true;
        }
    }

    public void takeDamage(int damage, Vector2 moveVector) {
        health -= damage;
        if (health <= 0) {
            this.moveVector = moveVector;
            StartCoroutine(DeathAnim());
        }
    }

    public void takeDamageFreeze(int damage, Vector2 moveVector, float freezeTime) {
        health -= damage;
        if (health <= 0) { 
            this.moveVector = moveVector;
            StartCoroutine(DeathAnim());
        } else {
            StartCoroutine(Frozen(freezeTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (isDead) {
            if (collision.gameObject.tag == "YWall") {
                moveVector *= new Vector2(1, -1);
            } else if (collision.gameObject.tag == "XWall") {
                moveVector *= new Vector2(-1, -1);
            }
        }
    }

    IEnumerator Frozen(float freezeTime) {
        frozen = true;
        yield return new WaitForSeconds(freezeTime);
        frozen = false;
        yield return null;
    }

    IEnumerator DeathAnim() {
        isDead = true;
        col.isTrigger = false;
        moveVector *= knockback;
        rb.velocity = moveVector;
        for (int i = 0; i < deathAnimTime * 100; i++) {
            transform.eulerAngles = Vector3.forward * i * knockback * -2;
            rb.velocity = moveVector;
            yield return new WaitForSeconds(deathAnimTime / 100);
        }
        spriteRenderer.enabled = false;
        particle.Play();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.enemyList.Remove(this);
        player.Score();
        Destroy(gameObject);
        yield return null;
    }
}
