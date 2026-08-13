using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Arrowspawner : MonoBehaviour
{
    public float FallSpeed;

    public bool HasStarted;

	public Transform nappi;
	public List<GameObject> ArrowList = new List<GameObject>();
	public KeyCode Arrow;
	public Slider bloodAmount;
	public GameObject nuoliPrefab;
	public ButtonManager Manager;
	public float spawnAika = 1.5f; 
	private float ajastin = 0f;
	private int Health = 3;
	private float missCooldown = 0.1f;
	private float missCooldownTimer = 0f;
	private float gameTimer = 0f;
	private float minSpawnTime = 1.2f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        SetupSpawner();
    }

	private void OnEnable()
	{
		SetupSpawner();
	}

	private void SetupSpawner()
	{
		HasStarted = false;
		gameTimer = 0f;
		ajastin = 0f;
		Health = 3;
	}

	private void CloseGame()
	{
		gameObject.SetActive(false);
		StingHit.StuckOnHuman = false;
		MinigameManager.IsMinigameActive = false;

		// Clear any remaining arrows
		if (ArrowList != null && ArrowList.Count > 0)
		{
			for (int i = ArrowList.Count - 1; i >= 0; i--)
			{
				if (ArrowList[i] != null)
				{
					Destroy(ArrowList[i]);
				}
			}
			ArrowList.Clear();
		}
	}

    // Update is called once per frame
    void Update()
    {
        if(!HasStarted)
        {
            if(Input.anyKeyDown)
            {
                HasStarted = true;
            }
        }
        else
        {
			gameTimer += Time.deltaTime;
			
			// Use the spawnAika value set in inspector for this spawner
			// Difficulty increases very slowly over time
			float currentSpawnAika = Mathf.Max(minSpawnTime, spawnAika - (gameTimer * 0.002f));
			
			ajastin += Time.deltaTime; 

			if (ajastin >= currentSpawnAika) 
			{
				GameObject uusiNuoli = Instantiate(nuoliPrefab, transform.position, nuoliPrefab.transform.rotation, transform);
				ArrowList.Add(uusiNuoli);
				ajastin = 0f;
			}

			if (ArrowList.Count > 0)
			{
				GameObject alinNuoli = ArrowList[0];

				if (alinNuoli.transform.position.y < nappi.position.y - 15f)
				{
					ArrowList.Remove(alinNuoli);
					Destroy(alinNuoli);
				}
			}
			
			missCooldownTimer -= Time.deltaTime;
			
			if (Input.GetKeyDown(Arrow))
			{
				if (ArrowList.Count > 0)
				{
					GameObject alinNuoli = ArrowList[0];
					TarkistaOsuma(alinNuoli.transform, nappi);
					missCooldownTimer = missCooldown;
				}
				else if (missCooldownTimer <= 0)
				{
					if (Manager != null)
					{
						Manager.TakeDamage();
					}
					else
					{
						Debug.LogWarning("Arrow pressed but no Manager assigned to Arrowspawner.");
					}
					missCooldownTimer = missCooldown;
				}
			}
		}
    }

	public void TarkistaOsuma(Transform putoavaNuoli, Transform nappi)
	{
		// Mathf.Abs luku aina pos
		float etaisyys = Mathf.Abs(putoavaNuoli.position.y - nappi.position.y);

		// Määritellään osuma-alueet etäisyyden perusteella
		if (etaisyys <= 5f)
		{
			//hyvä
			bloodAmount.value += 10;
			Mosquito.BloodAmount = bloodAmount.value;
			ArrowList.Remove(putoavaNuoli.gameObject);
			Destroy(putoavaNuoli.gameObject);

		}
		else if (etaisyys <= 10f)
		{
			// iha jees
			bloodAmount.value += 5;
			Mosquito.BloodAmount = bloodAmount.value;
			ArrowList.Remove(putoavaNuoli.gameObject);
			Destroy(putoavaNuoli.gameObject);

		}
		else if (etaisyys <= 15f)
		{
			// aika huono
			bloodAmount.value += 2;
			Mosquito.BloodAmount = bloodAmount.value;
			ArrowList.Remove(putoavaNuoli.gameObject);
			Destroy(putoavaNuoli.gameObject);
		}
		else
		{
			Manager.TakeDamage();
			ArrowList.Remove(putoavaNuoli.gameObject);
			Destroy(putoavaNuoli.gameObject);
		}
	}
}
