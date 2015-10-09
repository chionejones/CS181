using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour
{
    public OreType myType;

    // Use this for initialization
    void Start()
    {
        if (myType == OreType.Bronze)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (myType == OreType.Silver)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else if (myType == OreType.Gold)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (myType == OreType.Kryptonite)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
        if (myType == OreType.Bronze)
        {
            GameController.bronzeCount--;
            GameController.score += GameController.bronzePoints;
        }
        else if (myType == OreType.Silver)
        {
            GameController.silverCount--;
            GameController.score += GameController.silverPoints;
        }
        else if (myType == OreType.Gold)
        {
            GameController.goldCount--;
            GameController.score += GameController.goldPoints;
        }
        else if (myType == OreType.Kryptonite)
        {
            GameController.score += GameController.kryptonitePoints;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}