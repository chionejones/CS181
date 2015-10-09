using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject cubePrefab;
    bool cubeSpawned = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= 3.0f && cubeSpawned == false)
       
            {
            Vector3 cubePosition = new Vector3(0.0f, 0.0f, 0.0f);
            Instantiate(cubePrefab, cubePosition, Quaternion.identity);
            cubeSpawned = true;
        }


    }
}
