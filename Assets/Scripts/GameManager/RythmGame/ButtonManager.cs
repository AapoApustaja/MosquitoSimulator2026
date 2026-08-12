using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button buttonLeft;
    public Button buttonUp;
    public Button buttonDown;
    public Button buttonRight;



	public KeyCode keyleft = KeyCode.Alpha1;
	public KeyCode keyup = KeyCode.Alpha2;
	public KeyCode keydown = KeyCode.Alpha3;
	public KeyCode keyright = KeyCode.Alpha4;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {


	}

	// Update is called once per frame
	void Update()
    {	
		if(Input.GetKeyDown(keyleft))
		{
			SelectButton(buttonLeft);
		}
		if (Input.GetKeyDown(keyup))
		{
			SelectButton(buttonUp);
		}
		if (Input.GetKeyDown(keydown))
		{
			SelectButton(buttonDown);
		}
		if (Input.GetKeyDown(keyright))
		{
			SelectButton(buttonRight);
		}


	}

	void SelectButton(Button button)
	{
		EventSystem.current.SetSelectedGameObject(button.gameObject);
		button.onClick.Invoke();
	}

}
