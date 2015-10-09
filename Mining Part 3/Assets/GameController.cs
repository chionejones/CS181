using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject cubePrefab;
    float spawnFrequency = 3.0f;
    float timeToAct = 0.0f;
    float spawnSilverTime = 12.0f;
    float stopSpawningTime = 6.0f;

    // Use this for initialization
    void Start()
    {
        timeToAct += spawnFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeToAct && Time.time < spawnSilverTime + stopSpawningTime)
        {
            GameObject myCube = (GameObject)Instantiate(cubePrefab, new Vector3(Random.Range(-9f, 9f), Random.Range(-3f, 5f), 0), Quaternion.identity);
            if (Time.time >= spawnSilverTime)
            {
                myCube.GetComponent<Renderer>().material.color = Color.white;
            }
            myCube.GetComponent<Renderer>().material.color = Color.red;
            timeToAct += spawnFrequency;
        }

    }
}

