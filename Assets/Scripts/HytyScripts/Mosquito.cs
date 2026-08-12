using Unity.VisualScripting;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public static Mosquito Instance { get; private set; }


    public static float BloodAmount;

    // Pankkijutskat
    public static bool BloodCollectorUnlocked = false;
    public static float BloodInBank;
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
}