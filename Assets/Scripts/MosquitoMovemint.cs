using UnityEngine;

public class MosquitoMovemint : MonoBehaviour
{

	public Rigidbody rb;

	[SerializeField] private float responsiviness = 50f;
	[SerializeField] private float throttleAmount = 100f;

	private float throttle;
	private Vector3 rotationTorque;
	private float roll;
	private float pitch;
	private float yaw;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Rigidbodyyn k‰ytet‰‰ fixed
    void Update()
    {
		HandleInputs();

	}

	private void FixedUpdate()
	{
		rb.AddForce(transform.up * throttle, ForceMode.Impulse);

		rb.AddTorque(transform.right * pitch * responsiviness);
		rb.AddTorque(-transform.forward * roll * responsiviness);
		rb.AddTorque(transform.up * yaw * responsiviness);

	}

	private void HandleInputs()
	{
		roll = Input.GetAxis("Horizontal");
		pitch = Input.GetAxis("Vertical");
		yaw = Input.GetAxis("Yaw");

		if(Input.GetKey(KeyCode.UpArrow))
		{
			throttle += Time.deltaTime * throttleAmount;
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			throttle -= Time.deltaTime * throttleAmount;
		}
		throttle = Mathf.Clamp(throttle, -200f, 200f);

		if(Input.GetKey(KeyCode.Space))
		{
			rotationTorque = new Vector3(0, 0, 1);
		}
		else
		{
			rotationTorque = new Vector3(0, 0, 0);
		}
	}
}
    