using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

	public GameObject MainMenus;
	public GameObject SettingsMenus;
	public GameObject ControlsMenus;
	public GameObject VolumeMenus;

	public void Sens()
	{
		VolumeMenus.SetActive(true);
		SettingsMenus.SetActive(false);
	}

	public void Options()
	{
		SettingsMenus.SetActive(true);
		MainMenus.SetActive(false);

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
