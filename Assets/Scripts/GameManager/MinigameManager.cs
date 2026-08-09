using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static bool IsMinigameActive = false;

    public static void StartMinigame()
    {
        IsMinigameActive = true;
    }

    public static void EndMinigame()
    {
        IsMinigameActive = false;
    }
}