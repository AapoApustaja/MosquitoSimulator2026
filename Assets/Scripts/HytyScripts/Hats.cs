using UnityEngine;

public class Hats : MonoBehaviour
{

    [SerializeField] private Transform hatParent;

    private GameObject currentHat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadHat(string hatName)
    {
        // Poista nykyne
        if (currentHat != null)
        {
            Destroy(currentHat);
        }

        // lata hatun modeli
        GameObject hatPrefab = Resources.Load<GameObject>("Hats/" + hatName);

        // hatun luonti
        currentHat = Instantiate(hatPrefab, hatParent, false);
    }

    public void RemoveHat()
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
        }
    }
}
