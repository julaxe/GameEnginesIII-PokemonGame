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
        startButton.onClick.AddListener(StartButtonClick);
        quitButton.onClick.AddListener(QuitButtonClick);
    }

    public void StartButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
