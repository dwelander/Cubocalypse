using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RectTransform healthBar;
    public Player player;

    private void Update() {
        healthBar.localScale = new Vector3((float)player.health / player.maxHealth, 1, 1);
    }
}
