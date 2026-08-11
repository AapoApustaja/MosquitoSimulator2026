using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuckGame : MonoBehaviour
{
    public SuckGame suckGaem;
    public GameObject bodycapacity_;
    public GameObject Timer_;
    public GameObject BloodAmount_;
    public GameObject suckpressure_;
    public GameObject suckGameobj;

	[SerializeField] private AudioClip GameStart;


	public Slider time;
    public Slider pressure;
    public Slider capacity;
    public Slider bloodAmount;

    private float pressureAmount = 1f;
	private float pressureAmountDown = 0.3f;

    private float capacityAmoundDown = 4f;
    private float pressureMulti = 0.1f;

    private float bloodAmountMulti = 0.001f;

	private float MaxCapacity = 1000;
    private float MaxPressure = 100;
    private float MaxTime = 100;
	private float MaxBloodAmount = 100 ;

    private float Cooldown = 0.1f;
    private float CoolDownTimer = 0f; 

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        SetupGame();
	}

	private void OnEnable()
	{
        SetupGame();
        SoundFxManager.instance.PlaySoundFxClip(GameStart, transform, 1f);

	}

	private void CloseGame()
    {
        time.value = 0f;
        capacity.value = 0f;
        pressure.value = 0f;
		bodycapacity_.SetActive(false);
		Timer_.SetActive(false);
		suckpressure_.SetActive(false);
		suckGameobj.SetActive(false);
		suckGaem.enabled = false;
	}

	private void loseGame()
	{

		StingHit.StuckOnHuman = false;
		MinigameManager.IsMinigameActive = false;

        // veri nolliks jos kuolee
        Mosquito.BloodAmount = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    // Päivittää verimittarin ku vaihtaa sceneä
    public void UpdateBar()
    {
        bloodAmount.maxValue = MaxBloodAmount;
        bloodAmount.value = Mosquito.BloodAmount;
    }

	private void SetupGame()
    {
		time.maxValue = MaxTime;
		pressure.maxValue = MaxPressure;
		capacity.maxValue = MaxCapacity;
		bloodAmount.maxValue = MaxBloodAmount;

        // Ottaa hyttysen veren
        bloodAmount.value = Mosquito.BloodAmount;
		
        bodycapacity_.SetActive(true);
		Timer_.SetActive(true);
		BloodAmount_.SetActive(true);
		suckpressure_.SetActive(true);
		suckGaem.enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
        CoolDownTimer -= Time.deltaTime;
		pressure.value -= pressureAmountDown;
		capacity.value -= capacityAmoundDown;
		capacity.value += pressure.value * pressureMulti;
        bloodAmount.value += pressure.value * bloodAmountMulti;

        Mosquito.BloodAmount = bloodAmount.value;

        if(capacity.value >= MaxCapacity)
        {

            loseGame();

		}

        if (Input.GetKey(KeyCode.LeftShift))
        {
			CloseGame();
        }

        if(time.value >= MaxTime)
        {
            loseGame();
        }

		if (Input.GetKey(KeyCode.Space) && pressure.value <= MaxPressure)
        {
            
            pressure.value += pressureAmount;

		}

        if(CoolDownTimer <= 0f)     
        {
            time.value += 0.8f;
 
            CoolDownTimer = Cooldown;
        }
    }
}
