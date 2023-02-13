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
        if (collision.tag == "Enemy") {
            if (playerMovement.isDashing) {
                Destroy(collision.gameObject);
                player.score += 10;
                Debug.Log(player.score);
            } else {
                player.health -= 10;
                Debug.Log(player.health);
            }
        }
    }
}
