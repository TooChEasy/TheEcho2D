using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    PlayerController player;
    Sprite full;
    Sprite half;
    Sprite empty;
    Image[] healthImages;
    public Canvas hearthPanel;
    // public Canvas gameOver;

    void Awake () {
        player = gameObject.GetComponent<PlayerController>();
        full = Resources.Load<Sprite>("HealthFull");
        half = Resources.Load<Sprite>("HealthHalf");
        empty = Resources.Load<Sprite>("HealthEmpty");
    }
    public void SetHealthPanel (Canvas panel) {
        hearthPanel = panel;
        healthImages = hearthPanel.GetComponentsInChildren<Image>();
    }
    public void UpdateHealth (float healthPoints) {
        if (hearthPanel != null)
        {
            if (healthPoints == 5) {
                healthImages[0].overrideSprite = half;
            }
            if (healthPoints == 4) {
                healthImages[0].overrideSprite = empty;
            }
            if (healthPoints == 3) {
                healthImages[1].overrideSprite = half;
            }
            if (healthPoints == 2) {
                healthImages[1].overrideSprite = empty;
            }
            if (healthPoints == 1) {
                healthImages[2].overrideSprite = half;
            }
            if (healthPoints == 0) {
                healthImages[2].overrideSprite = empty;
                GameManager.Instance.TogglePause();
                GameManager.Instance.SetGameOver();
            }
        } else {
            Debug.LogWarning("Missing hearth panel!");
        }
    }
    public void ResetHealth () {
        foreach (Image heartImage in healthImages)
        {
            heartImage.overrideSprite = full;
        }
    }
}
