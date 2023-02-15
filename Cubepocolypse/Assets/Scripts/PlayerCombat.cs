using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Rigidbody2D rb;
    public float hitVelocity = 20f;
    public Player player;
    public PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (collision.tag == "Enemy") {
            if (playerMovement.isDashing) {
                enemy.takeDamage(player.damage);
            } else {
                player.takeDamage(enemy.damage);
            }
        }
    }
}
