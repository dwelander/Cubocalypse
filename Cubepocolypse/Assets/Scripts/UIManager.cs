using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform healthBar;
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
}
