using System;
using UnityEngine;

public class StingHit : MonoBehaviour
{
    bool stuck = false;
	Rigidbody rb;
	Vector3 direction;
	float cooldown = 2f;
	float cooldownTimer = 0f;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		rb = GetComponentInParent<Rigidbody>();
	}

    // Tarkistetaan pistimen osumat (other antaa tiedot mihin tˆrm‰tty)
	private void OnTriggerEnter(Collider other)
	{
		// tehd‰‰n suckable muuttuja
        Suckable IsSuckable = other.GetComponent<Suckable>();

        // jos lˆytyy suckable componentti niin edet‰‰
        if (IsSuckable != null)
        {
			stuck = true;
			// haetaan hyttysen body ja j‰hmetet‰‰n
	
			rb.isKinematic = true;

			// Tehd‰‰n hytyst‰ toisen objektin lapsi
			transform.parent.SetParent(other.transform);

			//Lasketaan hytyn ja objektin v‰linen et‰isyys
			direction = transform.parent.position - other.transform.position;
		

		}


	}
	// Update is called once per frame
	void Update()
    {


		if (stuck)
		{
			//Aloteitaan suki suki
			if (Input.GetKey(KeyCode.Space))
			{
				Console.WriteLine("moi");
			}


			// l‰hdet‰‰n irti
			else if (Input.GetKey(KeyCode.LeftShift))
			{
				stuck = false;
				//Siirret‰‰n hyty pois sis‰lt‰
				transform.parent.position += direction * 0.1f;
				//Poistetaan hytyn parent
				transform.parent.SetParent(null);
				rb.isKinematic = false;




			}
		}
		
	}
}
