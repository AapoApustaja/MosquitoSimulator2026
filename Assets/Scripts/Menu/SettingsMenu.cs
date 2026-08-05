using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

	public GameObject MainMenus;
	public GameObject SettingsMenus;
	public GameObject ControlsMenus;
	public GameObject SensMenus;



	public void Sens()
	{
		SensMenus.SetActive(true);
		SettingsMenus.SetActive(false);
	}

	public void Controls()
	{
		ControlsMenus.SetActive(true);
		SettingsMenus.SetActive(false);
	}
	public void Back()
    {
		MainMenus.SetActive(true);
		SettingsMenus.SetActive(false);
	}
}
