using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float accuracy;
    public Explosion explosionPrefab;

    private Rigidbody2D rb;
    private Enemy target;
    private Vector2 targetPosition;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        int random = Random.Range(0, GameManager.Instance.enemyList.Count);
        while (target == null) {
            target = GameManager.Instance.enemyList[random];
        }
        target = GameManager.Instance.enemyList[random];
    }

    private void FixedUpdate() {
        rb.velocity = (targetPosition - rb.position) * speed;
        speed += 0.1f;
    }

    private void Update() {
        if (target != null) {
            targetPosition = target.transform.position;
        }

        if (Distance(rb.position, targetPosition) < accuracy) {
            Instantiate(explosionPrefab, rb.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private float Distance(Vector3 pos1, Vector3 pos2) {
        return Mathf.Sqrt(Mathf.Pow((pos1.x - pos2.x), 2) + Mathf.Pow((pos1.y - pos2.y), 2));
    }
}
