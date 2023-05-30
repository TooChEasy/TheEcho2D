using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashlightController : MonoBehaviour
{
    public Sprite[] batterySprites;
    public Image batteryImage;
    
    public float maxBatteryLife = 100f;
    public float batteryDrainRate = 1f;
    public float currentBatteryLife;

    public float rechargeDelay = 5f;
    private float idleTimer = 0f;

    LightRotation flashlight;
    public TextMeshProUGUI flashText;

    public float rechargeRate = 1f;
    private bool isRecharging = false;

    void Start() {
        currentBatteryLife = maxBatteryLife;
    }
    // Update is called once per frame
    private void Update()
    {
        if (flashlight != null) {
            if (flashlight.onOff && currentBatteryLife > 0)
            {
                flashText.text = "On";
                idleTimer = 0f;
                currentBatteryLife -= batteryDrainRate * Time.deltaTime;

                if (currentBatteryLife <= 0)
                {
                    currentBatteryLife = 0;
                    flashlight.onOff = false;
                }
            }
            if (!flashlight.onOff) {
                flashText.text = "Off";
                idleTimer += Time.deltaTime;
            }
            if (idleTimer >= rechargeDelay)
            {
                StartCoroutine(RechargeBatteryOverTime());
            }
            displayBattery();
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            if (batteryDrainRate > 0) {
                batteryDrainRate = 0;
            }
            else {
                batteryDrainRate = 1f;
            }
        }

    }
    public void SetFlashPanel () {
        flashlight = FindAnyObjectByType<LightRotation>();
    }
    public void displayBattery () {
        if (currentBatteryLife >= 75) {
            batteryImage.overrideSprite = batterySprites[0];
        }
        if (currentBatteryLife >= 50 && currentBatteryLife < 75) {
            batteryImage.overrideSprite = batterySprites[1];
        }
        if (currentBatteryLife >= 25 && currentBatteryLife < 50) {
            batteryImage.overrideSprite = batterySprites[2];
        }
        if (currentBatteryLife > 0 && currentBatteryLife < 25) {
            batteryImage.overrideSprite = batterySprites[3];
        }
        if (currentBatteryLife <= 0) {
            batteryImage.overrideSprite = batterySprites[4];
        }
    }
    public void RechargeBattery()
    {
        currentBatteryLife = maxBatteryLife;
    }
    public void ResetBattery()
    {
        currentBatteryLife = maxBatteryLife;
    }
    private IEnumerator RechargeBatteryOverTime()
{
    isRecharging = true;

    while (currentBatteryLife < maxBatteryLife)
    {
        flashText.text = "Recharging";
        currentBatteryLife += rechargeRate * Time.deltaTime;
        currentBatteryLife = Mathf.Clamp(currentBatteryLife, 0f, maxBatteryLife);

        yield return null;
    }

    isRecharging = false;
}
}
