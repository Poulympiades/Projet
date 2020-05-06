using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
	public Animator animator;
	public string isRunning = "isRunning";
	public string isStun = "isStun";

	void Start()
	{
		if(animator == null)
		{
			animator = GetComponent<Animator>();
		}
	}

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			animator.SetBool(isRunning, true);
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			animator.SetBool(isRunning, false);
		}
	}
}
