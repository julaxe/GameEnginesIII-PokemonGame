using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject battleCanvas;
    public Button resumeButton, quitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.active)
            {
                Resume();
            }
            else
            {
                Time.timeScale = 0;

                pauseCanvas.SetActive(true);

                resumeButton.Select();
            }
        }
    }


    public void Resume()
    {
        if (!battleCanvas.active)
        {
            Time.timeScale = 1;
        }

        pauseCanvas.gameObject.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
