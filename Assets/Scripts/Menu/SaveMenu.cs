using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{

    public GameObject MainMenus;
    public GameObject SaveMenus;
    public void LoadGame()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.InitializeGame();

            return;
        }

        // jos ei ollu sceneä
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.NewGame();
            // Initialize data persistence objects with new game data
            DataManager.instance.InitializeDataPersistence();
        }

        // Load the main game scene (scene 1)
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        MainMenus.SetActive(true);
        SaveMenus.SetActive(false);
    }
}
