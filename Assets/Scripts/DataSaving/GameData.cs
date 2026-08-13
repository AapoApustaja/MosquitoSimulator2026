using UnityEngine;

[System.Serializable]
public class GameData
{
    // Scene data
    public int LastSceneIndex = 1;

    public float BloodAmount = 0f;

    // Pankkijutskat
    public bool BloodCollectorUnlocked = false;
    public float BloodInBank = 0;
    public float BloodBankCapacity = 100f;
    public float BankCapacityLevel = 1f;
    public float BloodBankLevel = 1f;
    public float BloodMultiplier = 0.1f;

    // Upgradettavat jutut
    public float normalSpeed = 5.0f;
    public float MaxBloodAmount = 100f;
    public float bloodSuckMulti = 0.001f;
    public float speedLevel = 1f;
    public float CapLevel = 1f;
    public float MultiLevel = 1f;

    // base koristeet
    public bool CarpetOwned = false;
    public bool CarpetOn = false;
    public int currentCarpent = 0;

    public bool PaintingOwned = false;
    public bool PaintingOn = false;

    public Color selectedColor;

    // Hatut
    public bool HaloUnlocked = false;
    public bool CatEarsUnlocked = false;
    public bool DisguiseUnlocked = false;

    public GameData()
    {
        this.LastSceneIndex = 1;
        this.BloodAmount = 0f;

        // Pankkijutskat
        this.BloodCollectorUnlocked = false;
        this.BloodInBank = 0;
        this.BloodBankCapacity = 100f;
        this.BankCapacityLevel = 1f;
        this.BloodBankLevel = 1f;
        this.BloodMultiplier = 0.1f;

        // Upgradettavat jutut
        this.normalSpeed = 5.0f;
        this.MaxBloodAmount = 100f;
        this.bloodSuckMulti = 0.001f;
        this.speedLevel = 1f;
        this.CapLevel = 1f;
        this.MultiLevel = 1f;

        // base koristeet
        this.CarpetOwned = false;
        this.CarpetOn = false;
        this.currentCarpent = 0;

        this.PaintingOwned = false;
        this.PaintingOn = false;

        // Hatut
        this.HaloUnlocked = false;
        this.CatEarsUnlocked = false;
        this.DisguiseUnlocked = false;

    }

}
