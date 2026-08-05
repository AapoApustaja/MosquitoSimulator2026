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

    private float normalSpeed = 8.0f;
    private float normalTurnspeed = 40.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        if (ControlsMenu.MovementType == 1)
        {
            rb.useGravity = false;
            rb.freezeRotation = true;
            animator.SetBool("Flying", true);
        }
    }

    // Rigidbodyyn k‰ytet‰‰ fixed
    void Update()
    {
		CheckBoundaries();

        // Movementti ei toimi jos kiinni ihmisess‰
        if(!StingHit.StuckOnHuman)
        {
            UniversalInputs();
            
            // Normaali movementti
            if (ControlsMenu.MovementType == 1)
            {
                NormalInputs();
            }

            // Helikopteri
            if (ControlsMenu.MovementType == 2)
            {
                HelicopterInputs();
            }

            // future joku mix
            //if (SettingsMenu.MovementType == 32)
            //{
            //    HandleInputs();
            //}
        }

    }

	private void FixedUpdate()
	{
        if (ControlsMenu.MovementType == 2)
        {
            rb.AddForce(transform.up * throttle, ForceMode.Impulse);

            rb.AddTorque(transform.right * pitch * responsiviness);
            rb.AddTorque(-transform.forward * roll * responsiviness);
            rb.AddTorque(transform.up * yaw * responsiviness);
        }

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

                if (ControlsMenu.MovementType == 1)
                {
                    animator.SetBool("Flying", true);
                }
            }

            else
            {
                rb.useGravity = true;

                if(ControlsMenu.MovementType == 1)
                {
                    animator.SetBool("Flying", false);
                }

                else
                {
                    animator.SetBool("Flying", true);
                }

                
            }
        }
        
    }
    /// <summary>
    /// Normaalit
    /// </summary>
    private void NormalInputs()
    {
        // Hakee input pystyss‰ ja sivuttain
        float verticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");

        // liikutellaa
        transform.Translate(Vector3.forward * verticalInput * normalSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * HorizontalInput * normalSpeed * Time.deltaTime);

        // Ylˆsp‰in kun space
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * normalSpeed * Time.deltaTime, Space.World);
        }

        // alasp‰in ku control
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * normalSpeed * Time.deltaTime, Space.World);
        }

        //k‰‰ntyy ylˆs
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right, -normalTurnspeed * Time.deltaTime);
        }

        // K‰‰ntyy alas
        if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.Rotate(Vector3.right, normalTurnspeed * Time.deltaTime);
        }

        //k‰‰ntyy oikealle
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, normalTurnspeed* 1.5f * Time.deltaTime, Space.World);
        }

        // K‰‰ntyy vasemmalle
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -normalTurnspeed*1.5f * Time.deltaTime, Space.World);
        }
    }


    /// <summary>
    /// Helikopteri
    /// </summary>
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

            // Lentoanimaatio jos painovoima pois p‰‰lt‰
            if (!rb.useGravity)
            {
                animator.SetBool("Flying", true);
            }
            else
            {
                animator.SetBool("Flying", false);
            }
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
    