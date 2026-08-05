using UnityEngine;

public class SensMenu : MonoBehaviour
{
	public GameObject SettingsMenus;
	public GameObject SensMenus;

	public void Back()
	{
		SettingsMenus.SetActive(true);
		SensMenus.SetActive(false);
	}
}
