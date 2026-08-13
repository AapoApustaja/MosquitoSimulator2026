using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mosquito : MonoBehaviour, IDataPersistence
{
    public static Mosquito Instance { get; private set; }


    public static float BloodAmount = 0f;

    // Pankkijutskat
    public static bool BloodCollectorUnlocked = false;
    public static float BloodInBank = 0;
    public static float BloodBankCapacity = 100f;
    public static float BankCapacityLevel = 1f;
    public static float BloodBankLevel = 1f;
    public static float BloodMultiplier = 0.1f;

    // Upgradettavat jutut
    public static float normalSpeed = 5.0f;
    public static float MaxBloodAmount = 100f;
    public static float bloodSuckMulti = 0.001f;
    public static float speedLevel = 1f;
    public static float CapLevel = 1f;
    public static float MultiLevel = 1f;

    // base koristeet
    public static bool CarpetOwned = false;
    public static bool CarpetOn = false;
    public static int currentCarpent = 0;

    public static bool PaintingOwned = false;
    public static bool PaintingOn = false;

    public static Color selectedColor;

    // Hatut
    public static bool HaloUnlocked = false;
    public static bool CatEarsUnlocked = false;
    public static bool DisguiseUnlocked = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {

        // Passivinen veren keräys blood collectoriin
        // Kulkee hytyn mukana
        if (BloodCollectorUnlocked)
        {
            AddBloodToBank();
        }
    }


    private void AddBloodToBank()
    {
        if (BloodInBank < BloodBankCapacity)
        {
            BloodInBank += Time.deltaTime * BloodMultiplier;
        }

    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void LoadData(GameData data)
    {
        LoadData(data, true);
    }

    public void LoadDataWithoutScene(GameData data)
    {
        LoadData(data, false);
    }

    private void LoadData(GameData data, bool loadScene)
    {
        if (data == null)
        {
            Debug.LogError("GameData is null in Mosquito.LoadData!");
            return;
        }

        BloodAmount = data.BloodAmount;

        // Pankkijutskat
        BloodCollectorUnlocked = data.BloodCollectorUnlocked;
        BloodInBank = data.BloodInBank;
        BloodBankCapacity = data.BloodBankCapacity;
        BankCapacityLevel = data.BankCapacityLevel;
        BloodBankLevel = data.BloodBankLevel;
        BloodMultiplier = data.BloodMultiplier;

        // Upgradettavat jutut
        normalSpeed = data.normalSpeed;
        MaxBloodAmount = data.MaxBloodAmount;
        bloodSuckMulti = data.bloodSuckMulti;
        speedLevel = data.speedLevel;
        CapLevel = data.CapLevel;
        MultiLevel = data.MultiLevel;

        // base koristeet
        CarpetOwned = data.CarpetOwned;
        CarpetOn = data.CarpetOn;
        currentCarpent = data.currentCarpent;

        PaintingOwned = data.PaintingOwned;
        PaintingOn = data.PaintingOn;

        // Hatut
        HaloUnlocked = data.HaloUnlocked;
        CatEarsUnlocked = data.CatEarsUnlocked;
        DisguiseUnlocked = data.DisguiseUnlocked;

        // Load the saved scene only when loading game, not when saving
        if (loadScene && data.LastSceneIndex > 0)
        {
            SceneManager.LoadScene(data.LastSceneIndex);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.BloodAmount = BloodAmount;

        // Pankkijutskat
        data.BloodCollectorUnlocked = BloodCollectorUnlocked;
        data.BloodInBank = BloodInBank;
        data.BloodBankCapacity = BloodBankCapacity;
        data.BankCapacityLevel = BankCapacityLevel;
        data.BloodBankLevel = BloodBankLevel;
        data.BloodMultiplier = BloodMultiplier;

        // Upgradettavat jutut
        data.normalSpeed = normalSpeed;
        data.MaxBloodAmount = MaxBloodAmount;
        data.bloodSuckMulti = bloodSuckMulti;
        data.speedLevel = speedLevel;
        data.CapLevel = CapLevel;
        data.MultiLevel = MultiLevel;

        // base koristeet
        data.CarpetOwned = CarpetOwned;
        data.CarpetOn = CarpetOn;
        data.currentCarpent = currentCarpent;

        data.PaintingOwned = PaintingOwned;
        data.PaintingOn = PaintingOn;

        // Hatut
        data.HaloUnlocked = HaloUnlocked;
        data.CatEarsUnlocked = CatEarsUnlocked;
        data.DisguiseUnlocked = DisguiseUnlocked;

        // Save the current scene index
        data.LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

}