using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
	public GameObject SettingsMenus;
	public GameObject ControlsMenus;

	public static int MovementType = 1;


	public void Back()
	{
		SettingsMenus.SetActive(true);
		ControlsMenus.SetActive(false);
	}

	public void Normal()
	{
		MovementType = 1;
	}

	public void Helicopter()
	{
		MovementType = 2;

	}
}
