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

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Vector2 movement = player.transform.position - transform.position;
        float temp = Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y));
        movement /= temp;
        rb.velocity = movement * moveSpeed;
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
            player.Score();
        }
    }
}
