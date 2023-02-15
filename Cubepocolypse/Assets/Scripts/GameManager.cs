using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Enemy enemyPrefab;
    public Camera cam;
    public float enemySpawnCooldown = 5f;
    public float minEnemySpawnCooldown = 0.5f;

    private bool canSpawn = true;
    private List<Enemy> enemyList = new List<Enemy>();

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (canSpawn) {
            int signX = Random.Range(0, 2) * 2 - 1;
            int signY = Random.Range(0, 2) * 2 - 1;

            Vector2 random = new Vector2(Random.Range(1f, 1.5f) * signX, Random.Range(1f, 1.5f) * signY);
            Vector2 pos = random * new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);

            StartCoroutine(SpawnEnemy(pos));
        }
    }

    public void GameOver() {
        StartCoroutine(GameOverIE());
    }

    public void LifeDown() {
        while (enemyList.Count > 0) {
            if (enemyList[0] != null) {
                Destroy(enemyList[0].gameObject);
            }
            enemyList.RemoveAt(0);
        }

        enemySpawnCooldown += 2.5f;
    }

    IEnumerator SpawnEnemy(Vector2 pos) {
        canSpawn = false;
        Enemy enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemyList.Add(enemy);
        yield return new WaitForSeconds(enemySpawnCooldown);
        canSpawn = true;
        if (enemySpawnCooldown > minEnemySpawnCooldown) {
            enemySpawnCooldown -= 0.1f;
        }
        yield return null;
    }

    IEnumerator GameOverIE() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
