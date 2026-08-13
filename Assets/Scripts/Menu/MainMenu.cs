using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject MainMenus;
	public GameObject SettingsMenus;
	public GameObject SaveMenus;


	public void Playgame()
	{

        SaveMenus.SetActive(true);
        MainMenus.SetActive(false);
    }
	public void Options()
	{
		SettingsMenus.SetActive(true);
		MainMenus.SetActive(false);
		
	}
	public void QuitGame()
	{
		Application.Quit();
	}

}
