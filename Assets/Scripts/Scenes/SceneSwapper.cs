using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class SceneSwapper : MonoBehaviour
{
    private bool showEnterText;

    private GUIStyle labelStyle;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 1.0f;

    [SerializeField] private bool fadeIn = false;

    [SerializeField] private int SceneIndex = 0;

    [SerializeField] private string enterText = "Press E to enter";

    public Image image;

    void Start()
    {
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 64;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        if (fadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }

    }

    private void Update()
    {
        if (showEnterText && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeAndLoadScene(SceneIndex));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showEnterText = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showEnterText = false;
        }
    }

    void OnGUI()
    {
        // Base enter teksti
        if (showEnterText)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), enterText, labelStyle);
        }

    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        cg.alpha = end;
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        yield return StartCoroutine(
            FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f, fadeDuration)
        );

        SceneManager.LoadScene(sceneIndex);
    }

}