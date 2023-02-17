using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public int damage;
    public float time;
    public float size;
    public float freezeTime;
    public ParticleSystem particle;

    private CircleCollider2D col;

    private void Awake() {
        col = GetComponent<CircleCollider2D>();
        StartCoroutine(FreezeAction());
        particle.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            Enemy enemy = collision.GetComponent<Enemy>();
            Vector2 moveVector = enemy.transform.position - transform.position;
            enemy.takeDamage(damage, moveVector * 10);
        }
    }

    IEnumerator FreezeAction() {
        for (int i = 0; i < 100; i++) {
            col.radius += size / 100;
            yield return new WaitForSeconds(time / 100);
        }
        yield return new WaitForSeconds(1f - time);
        Destroy(gameObject);
        yield return null;
    }
}
