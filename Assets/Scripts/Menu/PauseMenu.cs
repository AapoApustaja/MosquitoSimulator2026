using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    // Static ni voi k‰ytt‰‰ si muuallaki if(PauseMenu.IsPaused)
    public static bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
     
    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        // pys‰ytt‰‰ ajat
        Time.timeScale = 0f;
        isPaused = true;
    }

	public void ResumeGame()
	{
		pauseMenu.SetActive(false);
		// pys‰ytt‰‰ ajat
		Time.timeScale = 1f;
        isPaused = false;
	}

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        // 0 = MainMenu
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
