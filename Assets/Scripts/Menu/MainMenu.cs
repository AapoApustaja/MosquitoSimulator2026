using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject MainMenus;
	public GameObject SettingsMenus;


	public void Playgame()
	{
		// 0 = Mainmenu 1 = Mainscene
		SceneManager.LoadScene(1);
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
