using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BloodCollector : MonoBehaviour
{

    private GUIStyle labelStyle;

    private bool nearObject = false;
    private bool UsingShop = false;

    private float CapacityUpgradeCost = 100;
    private float LevelUpgradeCost = 100;

    [SerializeField] private GameObject canvas;

    [SerializeField] private TMP_Text CurrentBloodText;
    [SerializeField] private Button UnlockButton;

    [SerializeField] private TMP_Text BloodInBankText;
    [SerializeField] private Button CashOutButton;

    [SerializeField] private Button CapacityUpgradeButton;
    private TMP_Text CaUpText;

    [SerializeField] private Button LevelUpgradeButton;
    private TMP_Text LeUpText;

    private SuckGame sukisuki;

    private void Awake()
    {
        // Voi päivittää verimittaria
        sukisuki = FindAnyObjectByType<SuckGame>(FindObjectsInactive.Include);

        CaUpText = CapacityUpgradeButton.GetComponentInChildren<TMP_Text>();

        LeUpText = LevelUpgradeButton.GetComponentInChildren<TMP_Text>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 64;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        canvas.SetActive(false);
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

        if (Mosquito.BloodCollectorUnlocked)
        {
            UpdateText();
        }

    }

    // Lisää massit tilille
    public void CashOut()
    {
        Mosquito.BloodAmount += Mosquito.BloodInBank;

        Mosquito.BloodInBank = 0;

        sukisuki.UpdateBar();
    }


    public void UpgradeCapacity()
    {
        CapacityUpgradeCost *= Mosquito.BankCapacityLevel;
        
        if (Mosquito.BloodAmount >= CapacityUpgradeCost)
        {
            Mosquito.BloodAmount -= CapacityUpgradeCost;

            Mosquito.BankCapacityLevel += 1;
            Mosquito.BloodBankCapacity += Mosquito.BloodBankCapacity;

            sukisuki.UpdateBar();

            CheckUpgradeLevels();
        }
    }

    public void UpgradeBloodRate()
    {
        LevelUpgradeCost *= Mosquito.BloodBankLevel;

        if (Mosquito.BloodAmount >=  LevelUpgradeCost)
        {
            Mosquito.BloodAmount -= LevelUpgradeCost;

            Mosquito.BloodBankLevel += 1;
            Mosquito.BloodMultiplier += Mosquito.BloodMultiplier;

            sukisuki.UpdateBar();

            CheckUpgradeLevels();
        }
    }

    private void SetupShop()
    {
        MinigameManager.IsMinigameActive = true;
        UsingShop = true;

        canvas.SetActive(true);

        if (Mosquito.BloodCollectorUnlocked)
        {
            UnlockButton.gameObject.SetActive(false);
            BloodInBankText.gameObject.SetActive(true);
            CashOutButton.gameObject.SetActive(true);
            LevelUpgradeButton.gameObject.SetActive(true);
            CapacityUpgradeButton.gameObject.SetActive(true);

            CheckUpgradeLevels();
        }

        else 
        {
            UnlockButton.gameObject.SetActive(true);
            BloodInBankText.gameObject.SetActive(false);
            CashOutButton.gameObject.SetActive(false);
            LevelUpgradeButton.gameObject.SetActive(false);
            CapacityUpgradeButton.gameObject.SetActive(false);

            UpdateText();
        }

    }

    private void CloseShop()
    {
        canvas.SetActive(false);
        MinigameManager.IsMinigameActive = false;
        UsingShop = false;
    }

    private void CheckUpgradeLevels()
    {
        if(Mosquito.BankCapacityLevel > 3)
        {
            CapacityUpgradeButton.gameObject.SetActive(false);
        }

        if (Mosquito.BloodBankLevel > 3)
        {
            LevelUpgradeButton.gameObject.SetActive(false);
        }
    }

    public void UnlockBloodCollector()
    {
        if (Mosquito.BloodAmount >= 100)
        {
            Mosquito.BloodCollectorUnlocked = true;

            Mosquito.BloodAmount -= 100;

            UnlockButton.gameObject.SetActive(false);

            BloodInBankText.gameObject.SetActive(true);
            CashOutButton.gameObject.SetActive(true);

            sukisuki.UpdateBar();

        }
    }
    private void UpdateText()
    {
        if (CurrentBloodText != null)
        {
            CurrentBloodText.text = "Current blood: " + (int)Mosquito.BloodAmount;
        }

        if (BloodInBankText != null)
        {
            BloodInBankText.text = "Blood in bank: " + (int)Mosquito.BloodInBank + " / " + (int)Mosquito.BloodBankCapacity;
        }

        if (CaUpText != null)
        {
            CaUpText.text = "Upgrade capacity - " + ((int)(CapacityUpgradeCost * Mosquito.BankCapacityLevel));
        }

        if (LeUpText != null)
        {
            LeUpText.text = "Upgrade speed - " + ((int)(LevelUpgradeCost * Mosquito.BloodBankLevel));
        }

        
    }

    void OnGUI()
    {
        // Base enter teksti
        if (nearObject && !UsingShop)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), "Press E to access Blood collector", labelStyle);
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
