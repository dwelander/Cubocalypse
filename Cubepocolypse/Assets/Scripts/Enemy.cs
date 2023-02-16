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

    private SpriteRenderer spriteRenderer;
    private bool isDead;
    private Vector2 moveVector;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (!isDead) {
            Vector2 movement = player.transform.position - transform.position;
            float temp = Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y));
            movement /= temp;
            rb.velocity = movement * moveSpeed;
        }
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            player.Score();
            StartCoroutine(DeathAnim());
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

    IEnumerator DeathAnim() {
        isDead = true;
        col.isTrigger = false;
        moveVector = player.GetComponent<Rigidbody2D>().velocity * knockback;
        rb.velocity = moveVector;
        for (int i = 0; i < deathAnimTime * 100; i++) {
            transform.eulerAngles = Vector3.forward * i * -10;
            rb.velocity = moveVector;
            yield return new WaitForSeconds(deathAnimTime / 100);
        }
        Destroy(gameObject);
        yield return null;
    }
}
