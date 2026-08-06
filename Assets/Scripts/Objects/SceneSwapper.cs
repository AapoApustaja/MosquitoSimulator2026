using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class SceneSwapper : MonoBehaviour
{
    private bool showBaseEnter;

    private GUIStyle labelStyle;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 1.0f;

    [SerializeField] private bool fadeIn = false;

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
        if (showBaseEnter && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeAndLoadScene(3));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showBaseEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showBaseEnter = false;
        }
    }

    void OnGUI()
    {
        // Base enter teksti
        if (showBaseEnter)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 1.3f, 100, 20), "Press E to enter base", labelStyle);
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