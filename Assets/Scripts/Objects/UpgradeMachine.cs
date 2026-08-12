using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeMachine : MonoBehaviour
{

    private GUIStyle labelStyle;

    private bool nearObject = false;
    private bool UsingShop = false;

    private float SpeedUpgradeCost = 100f;
    private float CapacityUpgradeCost = 100f;
    private float MultiUpgradeCost = 100f;

    [SerializeField] private GameObject canvas;

    [SerializeField] private TMP_Text CurrentBloodText;

    [SerializeField] private Button speedUpgradeButton;
    private TMP_Text speedText;
    [SerializeField] private TMP_Text speedMaxed;
    [SerializeField] private TMP_Text CurrentSpeed;
    
    [SerializeField] private Button CapacityUpgradeButton; 
    private TMP_Text capacityText;
    [SerializeField] private TMP_Text CapMaxed;
    [SerializeField] private TMP_Text CurrentCapacity;

    [SerializeField] private Button MultiUpgradeButton; 
    private TMP_Text multiUpgradeText;
    [SerializeField] private TMP_Text SuckMaxed;
    [SerializeField] private TMP_Text CurrentMulti;

    private SuckGame sukisuki;

    private void Awake()
    {
        sukisuki = FindAnyObjectByType<SuckGame>(FindObjectsInactive.Include);

        speedText = speedUpgradeButton.GetComponentInChildren<TMP_Text>();
        capacityText = CapacityUpgradeButton.GetComponentInChildren<TMP_Text>();
        multiUpgradeText = MultiUpgradeButton.GetComponentInChildren<TMP_Text>();

        SuckMaxed.gameObject.SetActive(false);
        CapMaxed.gameObject.SetActive(false);
        speedMaxed.gameObject.SetActive(false);

        CurrentSpeed.gameObject.SetActive(false);
        CurrentCapacity.gameObject.SetActive(false);
        CurrentMulti.gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 64;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;
    }

    // Update is called once per frame
    void Update()
    {
        if (nearObject && !UsingShop && Input.GetKeyDown(KeyCode.E))
        {
            SetupShop();
        }

        if (UsingShop && Input.GetKeyDown(KeyCode.LeftShift))
        {
            CloseShop();
        }

        if (UsingShop)
        {
            UpdateText();
        }
    }

    private void checkUpgradeLevels()
    {
        if(Mosquito.speedLevel > 3)
        {
            speedUpgradeButton.gameObject.SetActive(false);
            speedMaxed.gameObject.SetActive(true);
        }

        if(Mosquito.CapLevel > 3)
        {
            CapacityUpgradeButton.gameObject.SetActive(false);
            CapMaxed.gameObject.SetActive(true);
        }
        
        if(Mosquito.MultiLevel > 3)
        {
            MultiUpgradeButton.gameObject.SetActive(false);
            SuckMaxed.gameObject.SetActive(true);
        }
    }

    public void UpgradeSpeed()
    {
        SpeedUpgradeCost *= Mosquito.speedLevel;
        if (Mosquito.BloodAmount >= SpeedUpgradeCost)
        {
            Mosquito.BloodAmount -= SpeedUpgradeCost;

            Mosquito.speedLevel += 1;
            Mosquito.normalSpeed += 2;

            sukisuki.UpdateBar();
            checkUpgradeLevels();
        }
        
    }

    public void UpgradeCapacity()
    {
        CapacityUpgradeCost *= Mosquito.CapLevel;
        if (Mosquito.BloodAmount >= CapacityUpgradeCost)
        {
            Mosquito.BloodAmount -= CapacityUpgradeCost;

            Mosquito.CapLevel += 1;
            Mosquito.MaxBloodAmount += Mosquito.MaxBloodAmount;

            sukisuki.UpdateBar();
            checkUpgradeLevels();
        }
    }

    public void UpgradeSuck()
    {
        MultiUpgradeCost *= Mosquito.MultiLevel;
        if (Mosquito.BloodAmount >= MultiUpgradeCost)
        {
            Mosquito.BloodAmount -= MultiUpgradeCost;

            Mosquito.MultiLevel += 1;
            Mosquito.bloodSuckMulti += Mosquito.bloodSuckMulti;

            sukisuki.UpdateBar();
            checkUpgradeLevels();
        }
    }

    private void UpdateText()
    {

        if (CurrentBloodText != null)
        {
            CurrentBloodText.text = "Current blood: " + (int)Mosquito.BloodAmount;
        }

        if (speedText != null)
        {
            speedText.text = "Upgrade speed - " + ((int)(SpeedUpgradeCost * Mosquito.speedLevel));
        }

        if (capacityText != null)
        {
            capacityText.text = "Upgrade capacity - " + ((int)(CapacityUpgradeCost * Mosquito.CapLevel));
        }

        if (multiUpgradeText != null)
        {
            multiUpgradeText.text = "Upgrade suck multiplier - " + ((int)(MultiUpgradeCost * Mosquito.MultiLevel));
        }

        if (CurrentSpeed !=  null)
        {
            CurrentSpeed.text = "Current speed: " + (int)Mosquito.normalSpeed;
        }

        if (CurrentCapacity != null)
        {
            CurrentCapacity.text = "Current capacity: " + (int)Mosquito.MaxBloodAmount;
        }

        if (CurrentMulti != null)
        {
            CurrentMulti.text = "Current suck multiplier: " + Mosquito.bloodSuckMulti;
        }
    }
    private void SetupShop()
    {
        MinigameManager.IsMinigameActive = true;
        UsingShop = true;

        canvas.SetActive(true);

        CurrentSpeed.gameObject.SetActive(true);
        CurrentCapacity.gameObject.SetActive(true);
        CurrentMulti.gameObject.SetActive(true);

        checkUpgradeLevels();
    }

    private void CloseShop()
    {
        canvas.SetActive(false);
        MinigameManager.IsMinigameActive = false;
        UsingShop = false;
    }


    void OnGUI()
    {
        // Base enter teksti
        if (nearObject && !UsingShop)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), "Press E to access Upgrade machine", labelStyle);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearObject = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearObject = false;
        }
    }
}
