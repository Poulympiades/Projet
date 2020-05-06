using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public int listSize;
	public List<GameObject> Objets = new List<GameObject>();
	int random;

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			foreach (Transform child in transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			random = Random.RandomRange(0, listSize);
			GameObject selected = Objets[random];
			GameObject spawned = Instantiate(selected, transform.position, Quaternion.identity);
			spawned.transform.parent = transform;
		}
    }
}
