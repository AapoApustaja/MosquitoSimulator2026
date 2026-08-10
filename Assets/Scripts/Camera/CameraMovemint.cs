using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraMovemint : MonoBehaviour
{
    private Transform player;

    private Vector3 offset = new Vector3(0, 0.75f, -3f);

    /// <summary>
    /// Mitkä objektit törmää kameraan (tähän default aina)
    /// </summary>
    [SerializeField] private LayerMask collisionMask;

    /// <summary>
    /// Miten kauan kameralla kestää smoothata
    /// </summary>
    [SerializeField] private float smoothTime = 0.30f;

    /// <summary>
    /// Miten nopeesti kamera kääntyy rotationin mukana
    /// </summary>
    [SerializeField] private float rotationSpeed = 25f;

    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {

    }

    // Kamerat lateupdateen niin ei lagaa
    private void LateUpdate()
    {
        if (!MinigameManager.IsMinigameActive)
        {
            if (ControlsMenu.MovementType == 1)
            {
                standardCamera();
            }

            if (ControlsMenu.MovementType == 2)
            {
                helicopterCamera();
            }
        }

    }

    /// <summary>
    /// Normi kontrolleille kamera
    /// </summary>
    private void standardCamera()
    {
        // Haluttu kameran positio
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Alkuperänen kameran positio olettaa että ei ole mitään tiellä
        Vector3 targetPosition = desiredPosition;


        Vector3 dir = desiredPosition - player.position;

        float distance = dir.magnitude;

        dir.Normalize();

        // Kattoo onko halutun kamerapositionin ja pelaajan välillä mitää estettä
        if (Physics.SphereCast(player.position, 0.3f, dir, out RaycastHit hit, distance, collisionMask))
        {
            // Liikutetaan kameraa esteen eteen
            targetPosition = hit.point - dir * 0.3f;
        }

        // Smoothisti liikuttaa kameran haluttuun paikkaan
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Laskee rotaation joka liikuttaa kameran pelaajaa kohti 
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);

        // Liikutta kameran rotaation
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Helikopterille kamera
    /// </summary>
    private void helicopterCamera()
    {
        transform.position = player.position + player.rotation * offset;

        Vector3 forward = player.forward;

        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);

        transform.rotation = targetRotation;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject mosquito = GameObject.FindWithTag("Player");

        if (mosquito != null)
        {
            player = mosquito.transform;
        }
    }

}
