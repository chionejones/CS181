using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject cubePrefab;
	public GameObject NextCube;
	public GameObject activeCube;
	//public GameObject randomCube;
	public Color NextCubeColor;
	private GameObject[,] allCubes;
	int gridWidth = 8;
	int gridHeight = 5;
	bool gameOver = false;
	bool nowActive;
	bool scoreIncrease = false;
	int totalRows = 5;
	int totalColumns = 8;
	int samePoints = 10;
	int differentPoints = 5;
	int noPlusPoints = -1;
	int score = 0;
	Color centerColor;
	Color topColor;
	Color bottomColor;
	Color leftColor;
	Color rightColor;
	float timeToAct = 0.0f;
	float turnLength = 2.0f;
	float gameTime = 60.0f;
	Color[] colors = {Color.blue, Color.green, Color.yellow, Color.magenta, Color.red};



	// Use this for initialization
	void Start () {
		timeToAct += turnLength;

		//gives NextCube a color from the color array
		NextCube.GetComponent<Renderer>().material.color = colors[ Random.Range (0, colors.Length) ];

		//Generates NextCube
		//NextCube = (GameObject) Instantiate(cubePrefab, new Vector3(0, 3, 0), Quaternion.identity);
		//randomCube.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

		//make grid of cubes
		allCubes = new GameObject[gridWidth, gridHeight];
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				allCubes[x,y] = (GameObject) Instantiate(cubePrefab, new Vector3(x*1 - 10, y*1 - 9, 10), Quaternion.identity);
				allCubes[x,y].GetComponent<CubeBehavior>().x = x;
				allCubes[x,y].GetComponent<CubeBehavior>().y = y;
				allCubes[x,y].GetComponent<Renderer>().material.color = Color.white;
			}
		}

	}

	//this method gives us a random cube
	public GameObject RandomCube (int x) {
		return allCubes [Random.Range (0, totalColumns), x];

	}


	//this list and function are used to look for available, random cubes in the row. 

	public bool ValidCubesinRow (Color color, int x)      {
		GameObject cubeCheck;
		List <GameObject> validCubes = new List <GameObject> ();
		while (validCubes.Count < totalColumns) {
			cubeCheck = RandomCube (x);
			if (cubeCheck.GetComponent<Renderer>().material.color == Color.white)      {
				cubeCheck.GetComponent<Renderer>().material.color = color;
				return true;
			}
			else if (validCubes.Contains (cubeCheck) == false) {
				validCubes.Add (cubeCheck);
			}
		}
		return false;
	}


	//using int keyarraynumber in order to minimize the code i'm writing here. ending the code in reference to the list i made so cubes can fit into right spot on grid
	public void KeyInput () {
		int keyArrayNumber = -1;
		if (Input.GetKeyDown ("1")) {
			keyArrayNumber = 0;
		} 
		else if (Input.GetKeyDown ("2")) {
			keyArrayNumber = 1;
		} 
		else if (Input.GetKeyDown ("3")) {
			keyArrayNumber = 2;
		} 
		else if (Input.GetKeyDown ("4")) {
			keyArrayNumber = 3;
		} 
		else if (Input.GetKeyDown ("5")) {
			keyArrayNumber = 4;
		}
		if (keyArrayNumber != -1) {
			Color turnNext = NextCube.GetComponent<Renderer> ().material.color;
			if (ValidCubesinRow (turnNext, keyArrayNumber) == true) {
				NextCube.SetActive (false);
			} 
			else {
				print ("Lose!");
				gameOver = true;
			}
		}
		//Set cubes black if player does not make a move
		else {
			if (Time.time >= timeToAct)   {
				if (ValidCubesinRow (Color.black, Random.Range (0, totalRows)) == true) {
					if (score >= 1)   {
						score += noPlusPoints;
						scoreIncrease = true;
					}
					NextCube.SetActive (false);
				}
				else {
					print ("Lose!");
					gameOver = true;
				}
			}
		}
	}


	//this allows for any cubes that aren't white/black to be activated and deactivated through clicks.
	public void Activate (GameObject clickedCube)   {

		if (clickedCube.GetComponent<Renderer> ().material.color != Color.white &&
			clickedCube.GetComponent<Renderer> ().material.color != Color.black) {
			if (nowActive == false) {
				clickedCube.transform.localScale += new Vector3 (0.5f, 0.5f, 0);
				nowActive = true;
				activeCube = clickedCube;
			} 
			else if (clickedCube != activeCube) {
				clickedCube.transform.localScale += new Vector3 (+0.5f, +0.5f, 0);
				activeCube.transform.localScale += new Vector3 (-0.5f, -0.5f, 0);
				activeCube = clickedCube;
			}
			else {
				clickedCube.transform.localScale += new Vector3 (-0.5f, -0.5f, 0);
				nowActive = false;
				activeCube = clickedCube;
			}
		}

	}

	//should allow for the movement of a cube if an adjacent cube is clicked.
	public void Movement (GameObject clickedCube)   {
		int clickedx = clickedCube.GetComponent<CubeBehavior> ().x;
		int newx = activeCube.GetComponent<CubeBehavior> ().x;
		int clickedy = clickedCube.GetComponent<CubeBehavior> ().y;
		int newy = activeCube.GetComponent<CubeBehavior> ().y;

		if (clickedCube.GetComponent<Renderer>().material.color == Color.white && nowActive == true && 
			(clickedx != newx || clickedy != newy)) {
			//this checks for adjacent cubes to the center activated cube
			if (Mathf.Abs (clickedx - newx) <= 1 && Mathf.Abs (clickedy - newy) <= 1)    {
				clickedCube.GetComponent<Renderer>().material.color = activeCube.GetComponent<Renderer>().material.color;
				activeCube.GetComponent<Renderer>().material.color = Color.white;
				clickedCube.transform.localScale += new Vector3 (+0.5f, +0.5f, 0);
				activeCube.transform.localScale += new Vector3 (-0.5f, -0.5f, 0);
				activeCube = clickedCube;
			}
		}
	}

	//checks for same color plus. designating something to allCubes should get rid of the error.
	public void CheckForPlus() {
		for (int x = 1; x < gridWidth - 1; x++) {
			for (int y = 1; y < gridHeight - 1; y++) {
				if (allCubes[x,y].GetComponent<Renderer>().material.color != Color.white && allCubes[x,y].GetComponent<Renderer>().material.color != Color.black) {
					centerColor = allCubes[x,y].GetComponent<Renderer>().material.color;
					topColor = allCubes[x, y + 1].GetComponent<Renderer>().material.color;
					bottomColor = allCubes[x, y - 1].GetComponent<Renderer>().material.color;
					leftColor = allCubes[x - 1, y].GetComponent<Renderer>().material.color;
					rightColor = allCubes[x + 1, y].GetComponent<Renderer>().material.color;
					if (allCubes[x + 1, y].GetComponent<Renderer>().material.color == centerColor) {
						if (allCubes[x - 1, y].GetComponent<Renderer>().material.color == centerColor) {
							if (allCubes[x, y + 1].GetComponent<Renderer>().material.color == centerColor) {
								if (allCubes[x, y - 1].GetComponent<Renderer>().material.color == centerColor) {
									//add score points
									score += samePoints;
								}
							}
						}
					}
					//checks for different colored plus
					else if (allCubes[x + 1, y].GetComponent<Renderer>().material.color != Color.white && allCubes[x + 1, y].GetComponent<Renderer>().material.color != Color.black && allCubes[x + 1, y].GetComponent<Renderer>().material.color != centerColor && allCubes[x + 1, y].GetComponent<Renderer>().material.color != topColor && allCubes[x + 1, y].GetComponent<Renderer>().material.color != bottomColor && allCubes[x + 1, y].GetComponent<Renderer>().material.color != leftColor) {
						if (allCubes[x - 1, y].GetComponent<Renderer>().material.color != Color.white && allCubes[x - 1, y].GetComponent<Renderer>().material.color != Color.black && allCubes[x - 1, y].GetComponent<Renderer>().material.color != centerColor && allCubes[x - 1, y].GetComponent<Renderer>().material.color != topColor && allCubes[x - 1, y].GetComponent<Renderer>().material.color != bottomColor) {
							if (allCubes[x, y + 1].GetComponent<Renderer>().material.color != Color.white && allCubes[x, y + 1].GetComponent<Renderer>().material.color != Color.black && allCubes[x, y + 1].GetComponent<Renderer>().material.color != centerColor && allCubes[x, y + 1].GetComponent<Renderer>().material.color != bottomColor) {
								if (allCubes[x, y - 1].GetComponent<Renderer>().material.color != Color.white && allCubes[x, y - 1].GetComponent<Renderer>().material.color != Color.black && allCubes[x, y - 1].GetComponent<Renderer>().material.color != centerColor) {
									//add score points
									score += differentPoints;
								}
							}
						}
					}
				}
			}
		}
	}
	//int ColorAddition(Color arrayColor) {
	//if (arrayColor == Color.blue) {
	//return 1;
	//}
	//if (arrayColor == Color.green) {
	//return 10;
	//}
	//if (arrayColor == Color.magenta) {
	//return 100;
	//}
	//if (arrayColor == Color.yellow) {
	//return 1000;
	//}
	//if (arrayColor == Color.red) {
	//return 10000;
	//}
	//return 0;
	//}

	//method checks for pluses of different colors 
	//public bool CheckForDifferentPlus(int x, int y) {
	//x and y are the center cube
	//int arrayColorTotal = 0;

	//arrayColorTotal += ColorAddition (allCubes [x,y].GetComponent<Renderer> ().material.color);
	//arrayColorTotal += ColorAddition (allCubes [x, y + 1].GetComponent<Renderer> ().material.color);
	//arrayColorTotal += ColorAddition (allCubes [x, y - 1].GetComponent<Renderer> ().material.color);
	//arrayColorTotal += ColorAddition (allCubes [x + 1 ,y].GetComponent<Renderer> ().material.color);
	//arrayColorTotal += ColorAddition (allCubes [x - 1 ,y].GetComponent<Renderer> ().material.color);

	//if (arrayColorTotal == 11111) {
	//return true;
	//}
	//return false;
	//}
	//void UpdateScore() {
	//for (int x =1; x < gridWidth - 1; x++) {
	//for (int y =1; y< gridHeight - 1; y++){
	//score update for same color pluses
	//if (CheckForSamePlus (x,y)) {
	//score += samePoints;
	//}
	//score update for different color pluses
	//if (CheckForDifferentPlus (x,y)) {
	//score += differentPoints;
	//}
	//}
	//}






	// Update is called once per frame
	void Update () {
		if (Time.time < gameTime && gameOver == false) {
		}
		if (NextCube.activeSelf) {
			KeyInput ();
		}
		if (Time.time >= timeToAct)   {
			//this controls events that occur inside of the turn function - random cube spawns, score showing, etc.
			//NextCube.SetActive (true);
			//NextCube.GetComponent<Renderer>().material.color = colors[ Random.Range (0, colors.Length) ];
			timeToAct += turnLength;
			if (scoreIncrease == true)    {
				print ("Score " + score + ".");
			}
			scoreIncrease = false;
		}
		else {
			gameOver = true;
			if (score >= 1)   {
				print ("Win!");
			}
			else {
				print ("Lose!");
			}
		}
	}
}
//Special thanks to Joseph Mercado and Isaiah Mann for help with this code.



