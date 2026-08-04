using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

	public GameObject MainMenus;
	public GameObject SettingsMenus;

	public static int MovementType = 2;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public void Back()
    {
		MainMenus.SetActive(true);
		SettingsMenus.SetActive(false);
	}
}
