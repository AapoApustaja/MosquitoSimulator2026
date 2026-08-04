using UnityEngine;

public class CameraMovemint : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(0, 1.0f, -4f);



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (SettingsMenu.MovementType == 1)
        {
            standardCamera();
        }

        if (SettingsMenu.MovementType == 2)
        {
            helicopterCamera();
        }
    }

    private void standardCamera()
    {
        transform.position = player.position + player.rotation * offset;
        transform.LookAt(player);
    }

    private void helicopterCamera()
    {
        transform.position = player.position + player.rotation * offset;

        Vector3 forward = player.forward;

        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);

        transform.rotation = targetRotation;
    }

}
