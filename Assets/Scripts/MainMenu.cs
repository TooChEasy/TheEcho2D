using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverClip;
    private GameManager gameManager;
    public TextMeshProUGUI tmp; 
    public Slider slider;

   
    void Start() {
        // Scene targetScene = SceneManager.GetSceneByName("Game");
        // if (targetScene.isLoaded)
        // {
        //     GameObject[] gameObjects = targetScene.GetRootGameObjects();
        //     foreach (GameObject go in gameObjects)
        //     {
        //         GameManager gm = go.GetComponent<GameManager>();
        //         if (gm != null)
        //         {
        //             gameManager = gm;
        //             break;
        //         }
        //     }
        // }
        // else
        // {
        //     Debug.LogWarning("The target scene is not loaded.");
        // }

        // if (gameManager == null)
        // {
        //     Debug.LogWarning("GameManager instance not found.");
        // }
        gameManager = GameManager.Instance;
        tmp.text = gameManager.GetVolume().ToString("F0");
        slider.value = gameManager.GetVolume();
    }
    void Update()
    {
        if ( tmp != null && slider != null)
        {
            tmp.text = slider.value.ToString("F0");
        }
    }
    public void SaveVolume () {
        if (gameManager != null)
        {
            gameManager.SetVolume(slider.value);
        }
    }
    public void PlayHover() {
        audioSource.PlayOneShot(hoverClip);
    }
    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitApp() {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
