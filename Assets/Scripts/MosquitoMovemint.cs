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

    // 1 = Normaali 2 = Helilupteri 3 = let him cook

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Rigidbodyyn k‰ytet‰‰ fixed
    void Update()
    {
		CheckBoundaries();

        UniversalInputs();

        // Normaali movementti
        //if (SettingsMenu.MovementType == 1)
        //{
        //    HandleInputs();
        //}

        // Helikopteri
        if (SettingsMenu.MovementType == 2)
        {
            HelicopterInputs();
        }

        // future joku mix
        //if (SettingsMenu.MovementType == 32)
        //{
        //    HandleInputs();
        //}

    }

	private void FixedUpdate()
	{
		rb.AddForce(transform.up * throttle, ForceMode.Impulse);

		rb.AddTorque(transform.right * pitch * responsiviness);
		rb.AddTorque(-transform.forward * roll * responsiviness);
		rb.AddTorque(transform.up * yaw * responsiviness);


	}

    /// <summary>
    /// Inputit jotka menee kaikkiin
    /// </summary>
    private void UniversalInputs()
    {

        // Painovoima p‰‰lle pois
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (rb.useGravity)
            {
                rb.useGravity = false;
            }

            else
            {
                rb.useGravity = true;
            }
        }
    }

	private void HelicopterInputs()
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
    