using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button buttonLeft;
    public Button buttonUp;
    public Button buttonDown;
    public Button buttonRight;

	public ImageChanger sydan1;
	public ImageChanger sydan2;
	public ImageChanger sydan3;

	public Arrowspawner ArrowspawnerLeft;
	public Arrowspawner ArrowspawnerRight;
	public Arrowspawner ArrowspawnerUp;
	public Arrowspawner ArrowspawnerDown;


	public KeyCode keyleft = KeyCode.Alpha1;
	public KeyCode keyup = KeyCode.Alpha2;
	public KeyCode keydown = KeyCode.Alpha3;
	public KeyCode keyright = KeyCode.Alpha4;

	Rigidbody rb;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		SetupManager();

        rb = FindAnyObjectByType<Rigidbody>(FindObjectsInactive.Include);
    }

	private void OnEnable()
	{
		SetupManager();

	}

	private void SetupManager()
	{
		Health = 3;

		if (sydan1 != null) sydan1.gameObject.SetActive(true);
		if (sydan2 != null) sydan2.gameObject.SetActive(true);
		if (sydan3 != null) sydan3.gameObject.SetActive(true);

		if (buttonLeft != null) buttonLeft.interactable = true;
		if (buttonUp != null) buttonUp.interactable = true;
		if (buttonDown != null) buttonDown.interactable = true;
		if (buttonRight != null) buttonRight.interactable = true;

		if (ArrowspawnerRight != null) ArrowspawnerRight.gameObject.SetActive(true);
		if (ArrowspawnerLeft != null) ArrowspawnerLeft.gameObject.SetActive(true);
		if (ArrowspawnerUp != null) ArrowspawnerUp.gameObject.SetActive(true);
		if (ArrowspawnerDown != null) ArrowspawnerDown.gameObject.SetActive(true);



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

	public int Health = 3;


	public void TakeDamage()
	{
		if (Health == 3)
		{
			sydan1.ChangePic();
			Health--;
		}
		else if (Health == 2)
		{
			sydan2.ChangePic();
			Health--;
		}
		else if (Health == 1)
		{
			CloseGame();

        }
	}

	private void CloseGame()
	{
		// Deactivate all arrow spawners
		if (ArrowspawnerLeft != null) ArrowspawnerLeft.gameObject.SetActive(false);
		if (ArrowspawnerRight != null) ArrowspawnerRight.gameObject.SetActive(false);
		if (ArrowspawnerUp != null) ArrowspawnerUp.gameObject.SetActive(false);
		if (ArrowspawnerDown != null) ArrowspawnerDown.gameObject.SetActive(false);

        rb.isKinematic = false;

        gameObject.SetActive(false);
        StingHit.StuckOnHuman = false;
        MinigameManager.IsMinigameActive = false;

        // veri nolliks jos kuolee
        Mosquito.BloodAmount = 0;

        Mosquito.HaloUnlocked = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	void SelectButton(Button button)
	{
		EventSystem.current.SetSelectedGameObject(button.gameObject);
		button.onClick.Invoke();
	}

}
