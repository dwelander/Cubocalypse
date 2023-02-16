using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake() {
        Instance= this;
    }

    public RectTransform healthBar;
    public RectTransform dashUI;
    public Player player;
    public GameObject[] livesBar = new GameObject[9];
    public Text text;

    private void Update() {
        healthBar.localScale = new Vector3((float)player.health / player.maxHealth, 1, 1);
        text.text = player.score.ToString();

        for (int i = 8; i > 0; i--) {
            if (i + 1 <= player.lives) {
                break;
            }

            if (livesBar[i] != null) {
                Destroy(livesBar[i]);
            }
        }
    }

    public IEnumerator DashUI(float dashCooldown) {
        for (float i = 0.1f; i <= 1.1f; i += 0.1f) {
            dashUI.localScale = new Vector3(i, i, 1);
            yield return new WaitForSeconds(dashCooldown / 10);
        }
        yield return null;
    }
}
