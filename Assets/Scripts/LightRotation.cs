using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightRotation : MonoBehaviour
{
    GameManager gameManager;
    public float rotationSpeed = 10f; // The speed at which the object rotates
    public bool onOff = true;
    private bool isLightChanged = false;
    private Light2D flashLight;
    private PolygonCollider2D polCol;
    private EnemyAI enemyScript;

    void Awake () {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start () {
        flashLight = GetComponent<Light2D>();
        polCol = GetComponent<PolygonCollider2D>();
        enemyScript = FindObjectOfType<EnemyAI>();
    }
    void Update()
    {
        if(!gameManager.isPaused) {
            if (Input.GetKeyDown(KeyCode.Space) && gameManager.isFlashCharged()) {
                onOff = !onOff;
                isLightChanged = true;
            }
            if (onOff && isLightChanged) {
                flashLight.intensity = 1;
                // flashLight.tag = "Light";
                // if (enemyScript != null) {
                //     enemyScript.FadeInCoroutine();
                // }
                isLightChanged = false;
            } 
            if (!onOff && isLightChanged || !gameManager.isFlashCharged()) {
                flashLight.intensity = 0;
                // flashLight.tag = "Untagged";
                // if (enemyScript != null) {
                //     enemyScript.FadeOutCoroutine();
                // }
                isLightChanged = false;
            }
            // Calculate the amount to rotate based on the mouse movement and rotation speed
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            float angle = Mathf.Atan2(mouseWorldPos.y - transform.position.y, mouseWorldPos.x - transform.position.x) * Mathf.Rad2Deg;

            // Rotate the object to face the mouse position
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            
            // Set the mouse position to be equal to the object's forward direction
            Vector3 forwardDir = transform.up;
            Vector3 targetMousePos = Camera.main.WorldToScreenPoint(transform.position + forwardDir);
            targetMousePos.z = mousePos.z;
            // Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            // Cursor.position = targetMousePos;
        }
    }
}
