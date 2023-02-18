using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;
    public float intitialSpeed;
    public float acceleration;
    public float accuracy;
    public float delay;
    public float lifetime;
    public Explosion explosionPrefab;

    private Camera cam;
    private Rigidbody2D rb;
    private Enemy target;
    private Vector2 targetPosition;
    private bool start = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
        bool temp = true;
        for (int i = 0; i < 10; i++) {
            int random = Random.Range(0, GameManager.Instance.enemyList.Count);
            target = GameManager.Instance.enemyList[random];
            if (target != null && target.onScreen) {
                temp = false;
                break;
            }
        }

        if (temp) {
            target = null;
            targetPosition = cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0));
        }

        Vector2 randomX = new Vector2(Random.Range(-0.2f, 0.2f), 0);

        rb.velocity = (Vector2.up + randomX) * intitialSpeed;
        StartCoroutine(missileDelay());
    }

    private void FixedUpdate() {
        if (start) {
            rb.velocity += (targetPosition - rb.position) * (speed / 100);
            speed += (acceleration / 100);
        }
    }

    private void Update() {
        Vector2 moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero) {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (target != null) {
            targetPosition = target.transform.position;
        }

        if (Distance(rb.position, targetPosition) < accuracy) {
            Instantiate(explosionPrefab, rb.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator missileDelay() {
        yield return new WaitForSeconds(delay);
        start = true;
        StartCoroutine(Destroy());
        yield return null;
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(lifetime);
        Instantiate(explosionPrefab, rb.position, Quaternion.identity);
        Destroy(gameObject);
        yield return null;
    }

    private float Distance(Vector3 pos1, Vector3 pos2) {
        return Mathf.Sqrt(Mathf.Pow((pos1.x - pos2.x), 2) + Mathf.Pow((pos1.y - pos2.y), 2));
    }
}
