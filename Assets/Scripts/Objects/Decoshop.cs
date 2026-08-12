using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Decoshop : MonoBehaviour
{
    private GUIStyle labelStyle;

    private bool nearObject = false;
    private bool UsingShop = false;

    [SerializeField] private GameObject canvas;

    [SerializeField] private TMP_Text CurrentBloodText;

    [SerializeField] private GameObject carpet;
    [SerializeField] private Button carpetButton;
    private TMP_Text carpetButtonText;

    [SerializeField] private Material[] carpetMaterials;
    [SerializeField] private Button CarpetMatButton;
    private Renderer carpetRenderer;
    private TMP_Text carpetMatButtonText;

    [SerializeField] private GameObject painting;
    [SerializeField] private Button PaintingButton;
    private TMP_Text PaintingButtonText;


    [SerializeField] private Material houseMaterial;
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private Image colorPreview;
    

    private SuckGame sukisuki;

    private void Awake()
    {
        sukisuki = FindAnyObjectByType<SuckGame>(FindObjectsInactive.Include);

        carpetButtonText = carpetButton.GetComponentInChildren<TMP_Text>();
        PaintingButtonText = PaintingButton.GetComponentInChildren<TMP_Text>();
        

        carpetRenderer = carpet.GetComponentInChildren<Renderer>();
        carpetMatButtonText = CarpetMatButton.GetComponentInChildren<TMP_Text>();

        redSlider.onValueChanged.AddListener(delegate { UpdateSelectedColor(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateSelectedColor(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateSelectedColor(); });

        canvas.gameObject.SetActive(false);

        CheckActiveDecos();
    }
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

    public void UseCarpetButton()
    {

        // Ostaa maton jos ei oo
        if (!Mosquito.CarpetOwned)
        {
            if(Mosquito.BloodAmount >= 100)
            {
                Mosquito.BloodAmount -= 100;

                Mosquito.CarpetOwned = true;

                sukisuki.UpdateBar();

                CarpetMatButton.gameObject.SetActive(true);
            }
        }

        // Jos matto omistuksessa niin voi laittaa päälle tai pois
        else
        {
            Mosquito.CarpetOn = !Mosquito.CarpetOn;
            carpet.SetActive(Mosquito.CarpetOn);
        }
    }

    public void UseCarpetMaterialButton()
    {

        Mosquito.currentCarpent++;

        if (Mosquito.currentCarpent >= carpetMaterials.Length)
        {
            Mosquito.currentCarpent = 0;
        }

        carpetRenderer.material = carpetMaterials[Mosquito.currentCarpent];
    }

    public void UsePaintingButton()
    { 
        // Ostaa taulun jos ei oo
        if (!Mosquito.PaintingOwned)
        {
            if (Mosquito.BloodAmount >= 100)
            {
                Mosquito.BloodAmount -= 100;

                Mosquito.PaintingOwned = true;

                sukisuki.UpdateBar();
            }
        }

        // Jos taulu omistuksessa niin voi laittaa päälle tai pois
        else
        {
            Mosquito.PaintingOn = !Mosquito.PaintingOn;
            painting.SetActive(Mosquito.PaintingOn);
        }
    }

    public void ApplyHouseColor()
    {
        houseMaterial.color = Mosquito.selectedColor;
    }
    private void UpdateSelectedColor()
    {
       Mosquito.selectedColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);

       UpdateColorPreview();
    }
    private void UpdateColorPreview()
    {
        colorPreview.color = Mosquito.selectedColor;
    }
    private void CheckActiveDecos()
    {
        if(Mosquito.CarpetOn)
        {
            carpet.gameObject.SetActive(true);
            carpetRenderer.material = carpetMaterials[Mosquito.currentCarpent];
        }

        if(Mosquito.PaintingOn)
        {
            painting.gameObject.SetActive(true);
        }

        if (houseMaterial != null)
        {
            Mosquito.selectedColor = houseMaterial.color;

            redSlider.value = Mosquito.selectedColor.r;

            greenSlider.value = Mosquito.selectedColor.g;

            blueSlider.value = Mosquito.selectedColor.b;

            UpdateColorPreview();
        }
    }
    private void UpdateText()
    {

        if (CurrentBloodText != null)
        {
            CurrentBloodText.text = "Current blood: " + (int)Mosquito.BloodAmount;
        }

        if (carpetButtonText != null)
        {
            if(!Mosquito.CarpetOwned)
            {
                carpetButtonText.text = "Purchase - 100";
            }

            else
            {

                carpetMatButtonText.text = "Carpet skin: " + (Mosquito.currentCarpent + 1);

                if (Mosquito.CarpetOn)
                {
                    carpetButtonText.text = "Disable";
                }
                else
                {
                    carpetButtonText.text = "Enable";
                }
            }
        }

        if (PaintingButtonText != null)
        {
            if (!Mosquito.PaintingOwned)
            {
                PaintingButtonText.text = "Purchase - 100";
            }

            else
            {
                if (Mosquito.PaintingOn)
                {
                    PaintingButtonText.text = "Disable";
                }
                else
                {
                    PaintingButtonText.text = "Enable";
                }
            }
        }
    }

    private void SetupShop()
    {
        MinigameManager.IsMinigameActive = true;
        UsingShop = true;

        canvas.SetActive(true);

        if(Mosquito.CarpetOwned)
        {
            CarpetMatButton.gameObject.SetActive(true);
        }
        else
        {
            CarpetMatButton.gameObject.SetActive(false);
        }
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
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), "Press E to decorate base", labelStyle);
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
