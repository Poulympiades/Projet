using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Construction : MonoBehaviour
{
    Vector3 additionalPos = Vector3.zero;
    Vector3 mouseDistance;
    float posX;
    float posY;
    Camera cam;
    Vector3 startPosition;
    Rigidbody gravity;
    [Range(0, 5)]
    public int scrollSpeed = 2;
	[Range(0, 10)]
	public int rotationSpeed = 10;
    [Range(0, 0.1f)]
    public float scalingSpeed = 0.05f;
    public string validItemTagName = "Valid";
    public GameObject Player;
	public GameObject ZoneConstruction;
	public GameObject particle;
    [Range(-1, 1)]
    public int scoreValue = 1;
    //PlayerScore playerScore;

    private void Start()
    {
        cam = Camera.main;
		Player = GameObject.Find("Player");
		//playerScore = Player.GetComponent<PlayerScore>();
		ZoneConstruction = GameObject.Find("Zone Construction");
	}

    void OnMouseDown()
    {
        mouseDistance = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - mouseDistance.x;
        posY = Input.mousePosition.y - mouseDistance.y;
        gravity = GetComponent<Rigidbody>();
        gravity.useGravity = false;
        gravity.freezeRotation = true;
    }
    void OnMouseDrag()
    {
        Vector3 camUp = cam.transform.up;
        Vector3 camRight = cam.transform.right;
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, mouseDistance.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos) + additionalPos;
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.RotateAround(camUp, -rotX);
            transform.RotateAround(camRight, rotY);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startPosition = Input.mousePosition;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
			float distance = Vector3.Distance(startPosition, Input.mousePosition);
            var scalingAmount = new Vector3(Mathf.Abs(distance), Mathf.Abs(distance), Mathf.Abs(distance));
            if (distance >= 10)
            {
                transform.localScale = scalingAmount * scalingSpeed/10;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (tag != validItemTagName)
            {
                tag = validItemTagName;
                Valid();
            }
            else if (tag == validItemTagName)
            {
                tag = "Construction";
                Unvalid();
            }
        }
        else
        {
			if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                additionalPos += cam.transform.forward * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            }
            transform.position = worldPos;
        }
    }
    void OnMouseUp()
    {
		if (tag != validItemTagName)
        {
            gravity.useGravity = true;
        }
        additionalPos = Vector3.zero;
    }

    void Valid()
    {
        gravity.velocity = Vector3.zero;
        gravity.useGravity = false;
		gravity.constraints = RigidbodyConstraints.FreezePosition;
		gravity.freezeRotation = true;
		transform.parent = Player.transform;
		particle.SetActive(false);
		//playerScore.score += scoreValue;
    }

    void Unvalid()
    {
        gravity.velocity = Vector3.zero;
        gravity.useGravity = true;
		gravity.constraints = RigidbodyConstraints.None;
		gravity.freezeRotation = false;
		transform.parent = ZoneConstruction.transform;
		particle.SetActive(true);
		//playerScore.score -= scoreValue;
    }
}