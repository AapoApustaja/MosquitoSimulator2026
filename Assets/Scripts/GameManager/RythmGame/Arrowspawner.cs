using System.Collections.Generic;
using UnityEngine;

public class Arrowspawner : MonoBehaviour
{
    public float FallSpeed;

    public bool HasStarted;

	public Transform nappi;
	public List<GameObject> ArrowList = new List<GameObject>();
	public KeyCode Arrow;

	public GameObject nuoliPrefab;
	public float spawnAika = 1.5f; 
	private float ajastin = 0f;

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
	
			ajastin += Time.deltaTime; 

			if (ajastin >= spawnAika) 
			{
				GameObject uusiNuoli = Instantiate(nuoliPrefab, transform.position, Quaternion.identity, transform);
				ArrowList.Add(uusiNuoli);
				ajastin = 0f;
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
			Destroy(putoavaNuoli.gameObject);
			ArrowList.Remove(putoavaNuoli.gameObject);
		}
		else if (etaisyys <= 5)
		{
			// iha jees
			Destroy(putoavaNuoli.gameObject);
			ArrowList.Remove(putoavaNuoli.gameObject);

		}
		else if (etaisyys <= 10f)
		{
			// aika huono
			Destroy(putoavaNuoli.gameObject);
			ArrowList.Remove(putoavaNuoli.gameObject);

		}
		else
		{
			//ohi
			
		}
	}
}
