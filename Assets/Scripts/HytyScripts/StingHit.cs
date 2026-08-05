using System;
using UnityEngine;

public class StingHit : MonoBehaviour
{
    public static bool StuckOnHuman = false;
	Rigidbody rb;
	Vector3 direction;
	float cooldown = 2f;
	float cooldownTimer = 0f;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		rb = GetComponentInParent<Rigidbody>();
	}

    // Tarkistetaan pistimen osumat (other antaa tiedot mihin törmätty)
	private void OnTriggerEnter(Collider other)
	{
		// tehdään suckable muuttuja
        Suckable IsSuckable = other.GetComponent<Suckable>();

        // jos löytyy suckable componentti niin edetää
        if (IsSuckable != null && cooldownTimer <= 0)
        {
            StuckOnHuman = true;
			// haetaan hyttysen body ja jähmetetään
	
			rb.isKinematic = true;

			// Tehdään hytystä toisen objektin lapsi
			transform.parent.SetParent(other.transform);

			//Lasketaan hytyn ja objektin välinen etäisyys
			direction = transform.parent.position - other.transform.position;


		}


	}
	// Update is called once per frame
	void Update()
    {
		cooldownTimer -= Time.deltaTime;

		if (StuckOnHuman)
		{
			//Aloteitaan suki suki
			if (Input.GetKey(KeyCode.Space))
			{
				// tähä jotai hienoo ny vaan toi läbäl
				transform.parent.localScale += new Vector3 (0.001f,0.001f,0.001f);
				
			}


			// lähdetään irti
			else if (Input.GetKey(KeyCode.LeftShift))
			{
                StuckOnHuman = false;

				//Siirretään hyty kaummaks
				transform.parent.position += direction * 0.1f;

				//Poistetaan hytyn parent
				transform.parent.SetParent(null);

				//Liike takas
				rb.isKinematic = false;

				// Cooldown et ei jää kii heti
				cooldownTimer = cooldown;

		




			}
		}
		
	}
}
