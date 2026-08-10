using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class BenchPress : MonoBehaviour
{
    private GUIStyle labelStyle;

    private bool nearObject = false;

    private bool PlayingGame = false;

    [SerializeField] private GameObject hyty;
    private Vector3 hytyPos;
    private Quaternion hytyRot;
    Rigidbody rb;

    [SerializeField] private GameObject bar;
    private Vector3 barPos;
    private Quaternion barRot;
    private Coroutine lowerBarCoroutine;

    [SerializeField] private GameObject camera;

    private int reps = 0;

    private float GameTimer = 0f;
    [SerializeField] private Slider TimeSlider;
    [SerializeField] private GameObject Timer_;

    private float CoolDownTimer = 0f;
    [SerializeField] private Slider BenchSlider;
    [SerializeField] private GameObject Bench_;
    [SerializeField] private TMP_Text RepsText;
    [SerializeField] private TMP_Text EarlyText;
    [SerializeField] private Image BenchFill;
    [SerializeField] private TMP_Text LeaveText;

    [SerializeField] private GameObject canvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = hyty.GetComponent<Rigidbody>();

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 64;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        Timer_.SetActive(false);
        Bench_.SetActive(false);
        canvas.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (nearObject && !PlayingGame && Input.GetKeyDown(KeyCode.E))
        {
            SetupGame();
        }

        // Itse peli
        if (PlayingGame)
        {
            // Ajastin
            GameTimer -= Time.deltaTime;
            TimeSlider.value = GameTimer;

            if (GameTimer <= 0)
            {
                EndGame();
            }

            // Penkkiaktiviteetti
            CoolDownTimer -= Time.deltaTime;
            BenchSlider.value = CoolDownTimer;

            // Väri punaseks jos menny liia vähä aikaa
            if (BenchSlider.value >= 2.0f)
            {
                BenchFill.color = Color.red;
            }
            else
            {
                BenchFill.color = Color.white;
            }

            // Space penkkaa
            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (BenchSlider.value >= 2.0f)
                {
                    EarlyText.text = "TOO EARLY!";
                }
                else
                {
                    RaiseBar();

                    reps++;
                    RepsText.text = "Reps: " + reps;
                    EarlyText.text = " ";
                }

                BenchSlider.value = BenchSlider.maxValue;
                CoolDownTimer = BenchSlider.value;
            }

            // Lähtö pelistää iteksee
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                EndGame();
            }
        }
    }

    private void SetupGame()
    {
        canvas.SetActive(true);

        SavePos();

        rb.isKinematic = true;

        SetPos();

        // Pelin aika jutut
        GameTimer = 30f;
        TimeSlider.maxValue = 30f;
        TimeSlider.value = GameTimer;
        Timer_.SetActive(true);

        // Penkki cooldown
        CoolDownTimer = 0f;
        BenchSlider.maxValue = 5f;
        BenchSlider.value = 0f;
        Bench_.SetActive(true);

        // Pelin alotus
        reps = 0;
        RepsText.text = "Reps: 0";
        EarlyText.text = " ";
        MinigameManager.IsMinigameActive = true;
        PlayingGame = true;

    }

    private void EndGame()
    {
        MinigameManager.IsMinigameActive = false;
        PlayingGame = false;

        if (lowerBarCoroutine != null)
        {
            StopCoroutine(lowerBarCoroutine);
            lowerBarCoroutine = null;
        }

        Timer_.SetActive(false);
        Bench_.SetActive(false);
        canvas.SetActive(false);

        RestorePos();

        rb.isKinematic = false;
    }

    void OnGUI()
    {
        if (nearObject && !PlayingGame)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), "Press E to Bench press", labelStyle);
        }

        if (PlayingGame)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), "Press Space to bench!", labelStyle);
        }

    }

    private void SavePos()
    {
        hytyPos = rb.position;
        hytyRot = rb.rotation;

        barPos = bar.transform.localPosition;
        barRot = bar.transform.localRotation;
    }

    private void SetPos()
    {
        // Hyty penkille
        hyty.transform.position = new Vector3(4.128f, 0.904f, 3.282f);
        hyty.transform.rotation = Quaternion.Euler(-165.0f, -90.05402f, 0.0f);

        // kamera paikoillee
        camera.transform.position = new Vector3(2.385f, 1.544f, 5.434f);
        camera.transform.rotation = Quaternion.Euler(15.709f, 135.0f, 0.0f);

        // tanko hytylle
        // tanko ylhäällä y 4.62
        bar.transform.localPosition = new Vector3(3.3f, 4.02f, -1.14f);
    }

    private void RaiseBar()
    {
        bar.transform.localPosition = new Vector3(3.3f, 4.62f, -1.14f);

        if (lowerBarCoroutine != null)
        {
            StopCoroutine(lowerBarCoroutine);
        }

        lowerBarCoroutine = StartCoroutine(LowerBar());
    }

    private IEnumerator LowerBar()
    {
        Vector3 startPos = bar.transform.localPosition;
        Vector3 targetPos = new Vector3(3.3f, 4.02f, -1.14f);

        float duration = 3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            bar.transform.localPosition = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        bar.transform.localPosition = targetPos;
        lowerBarCoroutine = null;
    }

    private void RestorePos()
    {
        rb.position = hytyPos;
        rb.rotation = hytyRot;

        bar.transform.localPosition = barPos;
        bar.transform.localRotation = barRot;
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
