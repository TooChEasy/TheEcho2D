using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    PlayerMovement player;
    Sprite full;
    Sprite half;
    Sprite empty;
    Image[] healthImages;
    public Canvas gameOver;

    void Awake () {
        player = FindObjectOfType<PlayerMovement>();
        full = Resources.Load<Sprite>("HealthFull");
        half = Resources.Load<Sprite>("HealthHalf");
        empty = Resources.Load<Sprite>("HealthEmpty");
        healthImages = gameObject.GetComponentsInChildren<Image>();
    }
    // void Start()
    // {
        
    // }
    void Update()
    {
        if (player.HealthPoints == 5) {
            healthImages[0].overrideSprite = half;
        }
        if (player.HealthPoints == 4) {
            healthImages[0].overrideSprite = empty;
        }
        if (player.HealthPoints == 3) {
            healthImages[1].overrideSprite = half;
        }
        if (player.HealthPoints == 2) {
            healthImages[1].overrideSprite = empty;
        }
        if (player.HealthPoints == 1) {
            healthImages[2].overrideSprite = half;
        }
        if (player.HealthPoints == 0) {
            healthImages[2].overrideSprite = empty;
            gameOver.gameObject.SetActive(true);
        }
    }
}
