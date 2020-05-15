using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	public string bumpables = "Bumpbables";
	public float bumpForce;
	public float bumpUp;

	void OnCollisionEnter(Collision collision)
	{
		Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
		if (collision.gameObject.CompareTag(bumpables))
		{
			if (rb)
			{
				rb.AddExplosionForce(bumpForce, transform.position, 10, bumpUp);
			}
		}
	}
}
