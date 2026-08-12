using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Arrowspawner : MonoBehaviour
{
    public float FallSpeed;

    public bool HasStarted;
	private int RandomNum;

	public Transform nappi;
	public List<GameObject> ArrowList = new List<GameObject>();
	public KeyCode Arrow;
	public Slider bloodAmount;
	public GameObject nuoliPrefab;
	public ButtonManager Manager;
	public float spawnAika = 1.5f; 
	private float ajastin = 0f;
	private int Health = 3;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
       
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
			RandomNum = Random.Range(5, 20);
			ajastin += Time.deltaTime; 

			if (ajastin >= spawnAika * RandomNum) 
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
			if (Input.GetKeyDown(Arrow))
			{
				if (ArrowList.Count > 0)
				{
					GameObject alinNuoli = ArrowList[0];
					TarkistaOsuma(alinNuoli.transform, nappi);
				}
			}
		}
    }

	public void TarkistaOsuma(Transform putoavaNuoli, Transform nappi)
	{
		// Mathf.Abs luku aina pos
		float etaisyys = Mathf.Abs(putoavaNuoli.position.y - nappi.position.y);

		// M‰‰ritell‰‰n osuma-alueet et‰isyyden perusteella
		if (etaisyys <= 1f)
		{
			//hyv‰
			bloodAmount.value += 20;
			Mosquito.BloodAmount = bloodAmount.value;
			ArrowList.Remove(putoavaNuoli.gameObject);
			Destroy(putoavaNuoli.gameObject);

		}
		else if (etaisyys <= 5)
		{
			// iha jees
			bloodAmount.value += 10;
			Mosquito.BloodAmount = bloodAmount.value;
			ArrowList.Remove(putoavaNuoli.gameObject);
			Destroy(putoavaNuoli.gameObject);

		}
		else if (etaisyys <= 10)
		{
			// aika huono
			bloodAmount.value += 5;
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
