using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static AudioController audioManager;
    private static SpawnManager spawnManager;

    public bool isPaused = false;
    public GameObject PausedMenu;

    public Texture2D cursorTexture;

    public Vector2 hotspot = Vector2.zero;

    public float volume = 100f;

    public GameObject playerPrefab;

    public Canvas ui;
    public Canvas pausedMenu;
    public Canvas overMenu;
    public Camera menuCamera;

    private Health health;
    private FlashlightController flash;


    public static GameManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        audioManager = AudioController.Instance;
        spawnManager = SpawnManager.Instance;
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuCamera.gameObject.activeSelf)
        {
            TogglePause();
        }
    }
    public float GetVolume() {
        return volume;
    }
    public void SetVolume(float vol) {
        volume = Mathf.Round(vol);
        float audioVolume = volume / 100f;
        if (audioManager != null)
        {
            audioManager.SetVolume(audioVolume);
        }
        if (audioManager == null)
        {
            Debug.LogError("AudioController object not found in the scene!");
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        if (PausedMenu != null)
        {
            PausedMenu.SetActive(isPaused);
        } else {
            Debug.LogWarning("Menu not found!");
        }
        
    }
    public void EndGame() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        List<GameObject> objectList = objects.ToList<GameObject>();
        objectList.Add(player);
        foreach (GameObject obj in objectList)
        {
            Destroy(obj);
        }
        health.ResetHealth();
        flash.ResetBattery();
    }
    public void StartGame()
    {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        GameObject playerGFX = player.transform.Find("PlayerGFX")?.gameObject;
        if (spawnManager != null) {
            spawnManager.CreateEnemies(playerGFX.transform);

        } else {
            Debug.LogError("Spawn manager object not found in the scene!");
        }
        if (playerGFX != null)
        {
            health = playerGFX.GetComponent<Health>();
            health.SetHealthPanel(ui.transform.Find("GamePanel").GetComponent<Canvas>());
            Camera playerCamera = playerGFX.GetComponentInChildren<Camera>();
            if (ui != null)
            {
                ui.worldCamera = playerCamera;
            }
            if (pausedMenu != null)
            {
                pausedMenu.worldCamera = playerCamera;
            }
            if (overMenu != null)
            {
                overMenu.worldCamera = playerCamera;
            }
            flash = FindFirstObjectByType<FlashlightController>();
            flash.SetFlashPanel();
        }
        else
        {
            Debug.LogWarning("Child object named 'PlayerGFX' not found.");
        }
    }
    public bool isFlashCharged () {
        return flash.currentBatteryLife > 0;
    }
    public void SetGameOver () {
        pausedMenu.gameObject.SetActive(false);
        overMenu.gameObject.SetActive(true);
    }
}
