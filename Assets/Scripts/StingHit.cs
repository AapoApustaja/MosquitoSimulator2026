using UnityEngine;

public class StingHit : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Tarkistetaan pistimen osumat (other antaa tiedot mihin törmätty)
	private void OnTriggerEnter(Collider other)
	{
		// tehdään suckable muuttuja
        Suckable IsSuckable = other.GetComponent<Suckable>();

        // jos löytyy suckable componentti niin edetää
        if (IsSuckable != null)
        {
            // haetaan hyttysen body ja jähmetetään
            Rigidbody rb = GetComponentInParent<Rigidbody>();
            rb.isKinematic = true;

            // laitetaan hyttynen kiinni objektiin
			transform.parent.SetParent(other.transform);

		}


	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
