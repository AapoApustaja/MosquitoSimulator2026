using System.Collections;
using UnityEngine;

public class MosquitoMovemint : MonoBehaviour
{

	public Rigidbody rb;

	private Animator animator;

	[SerializeField] private float responsiviness;
	[SerializeField] private float throttleAmount;

	private float throttle;
	private Vector3 rotationTorque;
	private float roll;
	private float pitch;
	private float yaw;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Rigidbodyyn k‰ytet‰‰ fixed
    void Update()
    {
		CheckBoundaries();

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

			// Lento p‰‰lle
			animator.SetBool("Flying", true);
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			throttle -= Time.deltaTime * throttleAmount;

			// Lento pois
			animator.SetBool("Flying", false);
		}
		throttle = Mathf.Clamp(throttle, -100f, 100f);

	}

	/// <summary>
	/// Tarkistaa rajat kartasta ja est‰‰ p‰‰syn
	/// </summary>
	private void CheckBoundaries()
	{

        // Negatiivine X
        if (transform.position.x < -90)
        {
            transform.position = new Vector3(-90, transform.position.y, transform.position.z);
        }

        // Positiivine X
        if (transform.position.x > 95)
        {
            transform.position = new Vector3(95, transform.position.y, transform.position.z);
        }

        // Negatiivine Z
        if (transform.position.z < -80)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -80);
        }

        // Positiivine Z
        if (transform.position.z > 95)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 95);
        }

        // Alternate versio myˆhemm‰lle
        //if(transform.position.x < -90 || transform.position.x > 95 || transform.position.z < -80 || transform.position.z > 95)
        //      {

        //          // Movementin poisto
        //          rb.linearVelocity = Vector3.zero;     
        //          rb.angularVelocity = Vector3.zero;

        //          // positio ja rotaatio resettaus
        //          transform.position = Vector3.zero;
        //          transform.rotation = Quaternion.identity;

        //          // ohjausjutut nolliks
        //          throttle = 0f;
        //          pitch = 0f;
        //          yaw = 0f;
        //          roll = 0f;

        //          // lentoanimaatio pois
        //          animator.SetBool("Flying", false);
        //      }
    }

}
    