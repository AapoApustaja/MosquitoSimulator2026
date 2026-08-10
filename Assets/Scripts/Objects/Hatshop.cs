using UnityEngine;
using UnityEngine.UI;
public class Hatshop : MonoBehaviour
{
    private GUIStyle labelStyle;

    private bool nearObject = false;
    private bool UsingShop = false;


    [SerializeField] private GameObject canvas;

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
    }

    // Laittaa hatun hytylle
    public void SelectHat(string hatName)
    {
        Mosquito.Instance.GetComponentInChildren<Hats>().LoadHat(hatName);
    }

    public void RemoveHat()
    {
        Mosquito.Instance.GetComponentInChildren<Hats>().RemoveHat();
    }

    private void SetupShop()
    {
        MinigameManager.IsMinigameActive = true;
        UsingShop = true;

        canvas.SetActive(true);
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
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), "Press E to buy hats", labelStyle);
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
