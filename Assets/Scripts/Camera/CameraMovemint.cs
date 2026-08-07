using UnityEngine;

public class CameraMovemint : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(0, 1.0f, -4f);

    [SerializeField] private LayerMask collisionMask;

    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private float rotationSpeed = 8f;

    private Vector3 velocity;

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
        if (ControlsMenu.MovementType == 1)
        {
            standardCamera();
        }

        if (ControlsMenu.MovementType == 2)
        {
            helicopterCamera();
        }
    }

    private void standardCamera()
    {
        Vector3 desiredPosition = player.position + player.rotation * offset;
        Vector3 targetPosition = desiredPosition;

        Vector3 dir = desiredPosition - player.position;
        float distance = dir.magnitude;
        dir.Normalize();

        if (Physics.SphereCast(player.position, 0.3f, dir, out RaycastHit hit, distance, collisionMask))
        {
            targetPosition = hit.point - dir * 0.3f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
    }

    private void helicopterCamera()
    {
        transform.position = player.position + player.rotation * offset;

        Vector3 forward = player.forward;

        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);

        transform.rotation = targetRotation;
    }

}
