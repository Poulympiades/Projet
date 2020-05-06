using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
	public float smoothSpeed;
	Vector3 velocity = Vector3.zero;

	private void Awake()
	{
		velocity = Vector3.zero;
	}
	void FixedUpdate()
    {
		Vector3 pos = new Vector3(target.position.x, target.position.y + distance ,target.position.z - distance);
		transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothSpeed);
    }
}