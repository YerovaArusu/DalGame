using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;

    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!isPaused) pauseGame();
            else resumeGame();

            isPaused = !isPaused;
        }
    }
    
    public void pauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void resumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
