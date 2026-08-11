using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public static Mosquito Instance { get; private set; }

    public static float BloodAmount;

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

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}