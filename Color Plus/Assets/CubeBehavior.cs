using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	public int x, y;


	GameController aGameController;
	void OnMouseDown () {
		//aGameController.ProcessClickedCube(this.gameObject, x, y

		GameController.Activate (gameObject);
		GameController.Movement (gameObject);
	}



	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {

	}
}