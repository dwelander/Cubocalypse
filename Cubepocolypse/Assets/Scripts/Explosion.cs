using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    public float size;
    public float time;

    private CircleCollider2D col;

    private void Awake() {
        col = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.takeDamage(damage);
            }
        }
    }

    IEnumerator Explode() {
        for (int i = 1;i < 10; i++) {
            col.radius += size / 10;
            transform.localScale = new Vector3(size / 10, size / 10, 1);
            yield return new WaitForSeconds(time / 10);
        }
        Destroy(this.gameObject);
        yield return null;
    }
}
