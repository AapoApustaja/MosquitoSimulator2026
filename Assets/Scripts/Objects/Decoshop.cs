using UnityEngine;
using UnityEngine.UI;

public class Decoshop : MonoBehaviour
{
    private GUIStyle labelStyle;

    private bool nearObject = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    }

    void OnGUI()
    {
        // Base enter teksti
        if (nearObject)
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
