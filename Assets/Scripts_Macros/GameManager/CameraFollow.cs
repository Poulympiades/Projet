using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject camera1;
	public GameObject camera2;
	public GameObject cameraCentre;
	public Transform player1;
	public Transform player2;
	public GameObject splitUI;
	public int cameraDistance;
	public float maxDistance;
	public float individualSmoothSpeed;
	public float generalSmoothSpeed;
	Vector3 velocity = Vector3.zero;
	bool oneTime;

	private void Awake()
	{
		oneTime = true;
		velocity = Vector3.zero;
	}
	void FixedUpdate()
	{
		float playerDistance = Vector3.Distance(player1.transform.position, player2.transform.position);
		Vector3 pos1 = new Vector3(player1.position.x, player1.position.y + cameraDistance, player1.position.z - cameraDistance);
		Vector3 pos2 = new Vector3(player2.position.x, player2.position.y + cameraDistance, player2.position.z - cameraDistance);
		Vector3 center = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
		Vector3 centerPos = new Vector3(center.x, center.y + cameraDistance, center.z - cameraDistance);
		cameraCentre.transform.position = Vector3.SmoothDamp(cameraCentre.transform.position, centerPos, ref velocity, generalSmoothSpeed);
		camera1.transform.position = Vector3.SmoothDamp(camera1.transform.position, pos1, ref velocity, individualSmoothSpeed);
		camera2.transform.position = Vector3.SmoothDamp(camera2.transform.position, pos2, ref velocity, individualSmoothSpeed);

		if (playerDistance > maxDistance)
		{
			splitUI.SetActive(true);
			cameraCentre.GetComponent<Camera>().rect = new Rect(0, 0, 0, 0);
			if (oneTime)
			{
				if (player1.transform.position.x > player2.transform.position.x)
				{
					camera1.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
					camera2.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
				}
				else
				{
					camera1.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
					camera2.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
				}
				oneTime = false;
			}
		}
		else
		{
			splitUI.SetActive(false);
			cameraCentre.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
			camera1.GetComponent<Camera>().rect = new Rect(0, 0, 0, 0);
			camera2.GetComponent<Camera>().rect = new Rect(0, 0, 0, 0);
			oneTime = true;
		}
	}
}