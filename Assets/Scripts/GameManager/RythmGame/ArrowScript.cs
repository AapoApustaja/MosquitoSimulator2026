using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    private float FallSpeed = 60f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position -= new Vector3(0f, FallSpeed * Time.deltaTime, 0f);
	}


}
