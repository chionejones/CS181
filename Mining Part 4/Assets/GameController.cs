using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject bronzePrefabCube;
    public GameObject silverPrefabCube;
    public GameObject goldPrefabCube;
    public GameObject kryptonitePrefabCube;
    private bool recentlySpawnedGold = false;
    private bool recentlySpawnedKryptonite = false;
    public static int bronzeCount = 0;
    public static int silverCount = 0;
    public static int goldCount = 0;
    public static int kryptoniteCount = 0;
    public static int bronzePoints = 1;
    public static int silverPoints = 10;
    public static int goldPoints = 100;
    public static int kryptonitePoints = 1000;
    public static int score = 0;
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
        if (Time.time >= timeToAct)
        {
            if (silverCount >= 1 && goldCount >= 1 && silverCount == goldCount || silverCount == goldCount + kryptoniteCount || goldCount == silverCount + kryptoniteCount)
            {
                Instantiate(kryptonitePrefabCube,
                new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 4f), 0), Quaternion.identity);
                recentlySpawnedKryptonite = true;
                recentlySpawnedGold = true;
                kryptoniteCount++;
            }
            else if (bronzeCount == 2 && silverCount == 2 && recentlySpawnedGold == false)
            {
                Instantiate(goldPrefabCube,
                new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 4f), 0),
                Quaternion.identity);
                goldCount++;
                recentlySpawnedGold = true;
            }
            else if (bronzeCount < 4)
            {
                Instantiate(bronzePrefabCube,
                new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 4f), 0),
                Quaternion.identity);
                bronzeCount++;
                recentlySpawnedGold = false;


            }
            else if (bronzeCount >= 4)
            {
                Instantiate(silverPrefabCube, new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 4f), 0), Quaternion.identity);
                silverCount++;
                recentlySpawnedGold = false;
                recentlySpawnedKryptonite = false;

            }



            timeToAct += spawnFrequency;
        }

    }
}



