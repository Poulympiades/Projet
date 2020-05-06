using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IA : MonoBehaviour
{
    public float wanderRange;
    public float speed;
    public float waitingTime;
    public float lookRadius;
    public GameObject playerHolder;
    public ParticleSystem particlesLove;
	public ParticleSystem particlesFlee;
	public enum State {None, Wander, Follow, Flee, Stopped};
    public State selection;
    Vector3 newPos;
    public float waitUntil = 6;
    public float fleeDuration = 0.5f;
	float fleeTimer = 0f;
    public PlayerScore playerScore;
	Rigidbody rigidbody;

    void Start()
    {
        selection = State.Wander;
        playerScore = FindObjectOfType<PlayerScore>();
        rigidbody = GetComponent<Rigidbody>();
        GeneratePos();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerHolder.transform.position, transform.position);

		if(selection != State.Stopped)
		{
			if (distanceToPlayer <= lookRadius)
			{
				if (playerScore.score > 0)
				{
					selection = State.Follow;
				}
				else if (playerScore.score < 0)
				{
					selection = State.Flee;
				}
				else if(selection != State.Wander && selection != State.None)
				{
					selection = State.Wander;
				}
			}
		}

		switch (selection)
        {
			case State.None:
				
			break;

            case State.Wander:
                {
					particlesLove.Stop();
					particlesFlee.Stop();
					waitUntil -= Time.deltaTime;
					rigidbody.AddForce(transform.forward * speed, ForceMode.Force);
					Vector3 look = new Vector3(newPos.x, transform.position.y, newPos.z);
                    transform.LookAt(look);
                    if (waitUntil < 0)
                    {
                        selection = State.None;
						StartCoroutine(Wait());
					}
                    if (transform.position == newPos)
                    {
                        selection = State.None;
						StartCoroutine(Wait());
					}
                }
            break;

            case State.Follow:
                {
					particlesLove.Play();
					rigidbody.AddForce(transform.forward * speed, ForceMode.Force);
					transform.LookAt(playerHolder.transform.position, Vector3.up);
                    if (Input.GetKey(KeyCode.Space))
                    {
                        selection = State.Stopped;
						StartCoroutine(Wait());
                    }
                    if (distanceToPlayer > lookRadius)
                    {
                        selection = State.Wander;
                    }
                }
            break; 

            case State.Flee:
                {
					particlesFlee.Play();
					Vector3 fleeDirection = transform.position - playerHolder.transform.position;
                    fleeDirection.y = rigidbody.velocity.y;
                    rigidbody.velocity = fleeDirection;
                    if(fleeTimer >= fleeDuration)
                    {
                        if (distanceToPlayer > lookRadius)
                        {
							selection = State.Wander;
                        }
                        else
                        {
							fleeTimer = 0f;
                        }
                    }
                    else
                    {
                        fleeTimer += Time.deltaTime;
                    }
                    transform.LookAt(transform.position + fleeDirection);


                }
			break;

			case State.Stopped:
				
			break;
        }
       
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitingTime);
        waitUntil = 6;
        GeneratePos();
    }

    public Vector3 GeneratePos()
    {
        Vector3 initialPos = transform.position;
        Vector3 randomPos = Random.insideUnitCircle * wanderRange;
        Vector3 XZPos = new Vector3(randomPos.x, 0, randomPos.y);
        newPos = initialPos + XZPos;
        float newPosDistance = Vector3.Distance(transform.position, newPos);
        if (Physics.Raycast(transform.position, newPos, newPosDistance))
        {
            GeneratePos();
        }
        else
        {
            selection = State.Wander;
        }
        return newPos;
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); 
    }
}
