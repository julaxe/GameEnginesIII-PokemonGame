using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    public Button startButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundManagerInstace.PlaySound("Background");
        startButton.onClick.AddListener(StartButtonClick);
        quitButton.onClick.AddListener(QuitButtonClick);
    }

    public void StartButtonClick()
    {
        SceneManager.LoadScene(1);
        SoundManager.soundManagerInstace.PlaySound("Select");
    }
    public void QuitButtonClick()
    {
        SoundManager.soundManagerInstace.PlaySound("Select");
        Application.Quit();
    }
}
