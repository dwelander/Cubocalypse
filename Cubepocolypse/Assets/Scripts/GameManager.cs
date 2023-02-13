using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Camera cam;
    public float enemySpawnCooldown = 5f;

    private bool canSpawn = true;

    private void Update() {
        if (canSpawn) {
            int signX = Random.Range(0, 2) * 2 - 1;
            int signY = Random.Range(0, 2) * 2 - 1;

            Vector2 random = new Vector2(Random.Range(1f, 1.5f) * signX, Random.Range(1f, 1.5f) * signY);
            Vector2 pos = random * new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);

            StartCoroutine(SpawnEnemy(pos));
        }
    }

    IEnumerator SpawnEnemy(Vector2 pos) {
        canSpawn = false;
        Enemy enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        yield return new WaitForSeconds(enemySpawnCooldown);
        canSpawn = true;
        if (enemySpawnCooldown > 1f) {
            enemySpawnCooldown -= 0.1f;
        }
        yield return null;
    }
}
