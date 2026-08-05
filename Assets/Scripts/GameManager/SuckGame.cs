using UnityEngine;
using UnityEngine.UI;

public class SuckGame : MonoBehaviour
{
    public SuckGame suckGaem;
    public GameObject bodycapacity_;
    public GameObject Timer_;
    public GameObject BloodAmount_;
    public GameObject suckpressure_;
    public GameObject suckGameobj;

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
        time.maxValue = MaxTime;
        pressure.maxValue = MaxPressure;
        capacity.maxValue = MaxCapacity;
        bloodAmount.maxValue = MaxBloodAmount;
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

        if(capacity.value == MaxCapacity)
        {
            bodycapacity_.SetActive(false);
            Timer_.SetActive(false);
            suckpressure_.SetActive(false);
			suckGameobj.SetActive(false);
			suckGaem.enabled = false;



		}

		if (Input.GetKey(KeyCode.Space) && pressure.value <= MaxPressure)
        {
            
            pressure.value += pressureAmount;

		}

        if(CoolDownTimer <= 0f)
        {
            time.value += 0.5f;
 
            CoolDownTimer = Cooldown;
        }
    }
}
