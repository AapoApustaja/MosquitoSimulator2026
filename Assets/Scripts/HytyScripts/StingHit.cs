using System;
using UnityEngine;

public class StingHit : MonoBehaviour
{
    public static bool StuckOnHuman = false;
	Rigidbody rb;
	Vector3 direction;
	float cooldown = 2f;
	float cooldownTimer = 0f;

	private SuckGame sukisuki;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

	}

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        sukisuki = FindAnyObjectByType<SuckGame>(FindObjectsInactive.Include);

		sukisuki.UpdateBar();
    }

    // Tarkistetaan pistimen osumat (other antaa tiedot mihin tˆrm‰tty)
    private void OnTriggerEnter(Collider other)
	{
		// tehd‰‰n suckable muuttuja
        Suckable IsSuckable = other.GetComponent<Suckable>();

        // jos lˆytyy suckable componentti niin edet‰‰
        if (IsSuckable != null && cooldownTimer <= 0)
        {
            StuckOnHuman = true;

			MinigameManager.IsMinigameActive = true;
            // haetaan hyttysen body ja j‰hmetet‰‰n

            rb.isKinematic = true;

			//// Tehd‰‰n hytyst‰ toisen objektin lapsi
			//transform.parent.SetParent(other.transform);

			//Lasketaan hytyn ja objektin v‰linen et‰isyys
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
				if (sukisuki == null)
				{
                    sukisuki = FindAnyObjectByType<SuckGame>(FindObjectsInactive.Include);
                }

                    sukisuki.gameObject.SetActive(true);
                    sukisuki.enabled = true;

			}


			// l‰hdet‰‰n irti
			else if (Input.GetKey(KeyCode.LeftShift))
			{
                StuckOnHuman = false;
				MinigameManager.IsMinigameActive = false;

                //Siirret‰‰n hyty kaummaks
                transform.parent.position += direction * 0.1f;

				//Poistetaan hytyn parent
				transform.parent.SetParent(null);

				//Liike takas
				rb.isKinematic = false;

				// Cooldown et ei j‰‰ kii heti
				cooldownTimer = cooldown;

			}
		}
		
	}
}
