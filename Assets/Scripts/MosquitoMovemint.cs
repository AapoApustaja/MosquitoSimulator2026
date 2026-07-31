using UnityEngine;

public class MosquitoMovemint : MonoBehaviour
{
    public float speed = 4.0f;
	public float rotationspeed = 20.0f;
	public float turnspeed = 20.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Hakee input pystyss‰ ja sivuttain
		float verticalInput = Input.GetAxis("Vertical");
		float HorizontalInput = Input.GetAxis("Horizontal");    

        // liikutellaa
		transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime); 
		transform.Translate(Vector3.right * HorizontalInput * speed * Time.deltaTime);

		//K‰‰nnet‰‰n
		transform.Rotate(Vector3.right * verticalInput * rotationspeed * Time.deltaTime);
		transform.Rotate(Vector3.back * HorizontalInput * rotationspeed * Time.deltaTime);

		// Ylˆsp‰in kun space
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}

		// alasp‰in ku control
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(Vector3.down * speed * Time.deltaTime);
		}

		//k‰‰ntyy oikealle
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Rotate(Vector3.up * turnspeed * Time.deltaTime);
		}

		// K‰‰ntyy vasemmalle
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			
			transform.Rotate(Vector3.up * -turnspeed * Time.deltaTime);
		}
	}
}
    