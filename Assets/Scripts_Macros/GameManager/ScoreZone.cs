using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class ScoreZone : MonoBehaviour
{
	public int actualScore;

	public void OnTriggerEnter(Collider collision)
	{
		if(collision.GetComponent<IAScore>())
		{
			int IAScore = collision.GetComponent<IAScore>().score;
			actualScore = actualScore + IAScore;
		}
	}

	public void OnTriggerExit(Collider collision)
	{
		if (collision.GetComponent<IAScore>())
		{
			int IAScore = collision.GetComponent<IAScore>().score;
			actualScore = actualScore - IAScore;
		}
	}
}
