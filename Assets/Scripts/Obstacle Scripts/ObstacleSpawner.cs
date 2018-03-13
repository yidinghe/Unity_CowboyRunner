using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

	[SerializeField]
	private GameObject[] obstacles;

	private List<GameObject> obstaclesForSpawing = new List<GameObject> ();


	void Awake ()
	{
		
	}

	void InitializeObstacles ()
	{
		int index = 0;
		for (int i = 0; i < obstacles.Length * 3; i++) {
			GameObject obj = Instantiate (obstacles [index], transform.position, Quaternion.identity) as GameObject;
			obstaclesForSpawing.Add (obj);
			obstaclesForSpawing [i].SetActive (false);
			index++;
			if (index == obstacles.Length)
				index = 0;
		}
	}
}
