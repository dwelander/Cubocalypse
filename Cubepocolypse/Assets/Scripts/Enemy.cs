using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    public Transform player;

    private void Awake() {
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate() {
        Vector2 movement = player.position - transform.position;
        float temp = Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y));
        movement /= temp;
        rb.velocity = movement * moveSpeed;
    }
}