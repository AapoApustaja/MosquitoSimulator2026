using UnityEngine;

public class Suckable : MonoBehaviour
{
    //Haetaan hyty
    public GameObject Hyty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        //Tunnistetaan ja otetaan hyty talteen sek‰ sen body
		Hyty = collision.gameObject;
        Rigidbody rb = Hyty.GetComponent<Rigidbody>();
        if(rb != null )
        {
            // j‰hmett‰‰ hyty paikalleen
			rb.isKinematic = true;

		}
		// liimataan hyttynen ja objekti yhteen parenting avulla
		collision.transform.SetParent(transform);

		//collision.gameObject.transform.SetParent(this.transform);

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
