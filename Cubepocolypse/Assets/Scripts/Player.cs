using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int lives = 9;
    public int score = 0;
    public int damage = 10;
    public int scoreIncrease = 10;

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            lives--;
            health = maxHealth;
            GameManager.Instance.LifeDown();
            transform.position = new Vector3(0, 0, 95);
        } 
        if (lives <= 0) {
            GameManager.Instance.GameOver();
        }
    }

    public void Score() {
        score += scoreIncrease;
        StartCoroutine(ScoreMultiplier());
    }

    IEnumerator ScoreMultiplier() {
        scoreIncrease = Mathf.RoundToInt(scoreIncrease * 1.25f);
        yield return new WaitForSeconds(1f);
        scoreIncrease = 10;
        yield return null;
    }
}
