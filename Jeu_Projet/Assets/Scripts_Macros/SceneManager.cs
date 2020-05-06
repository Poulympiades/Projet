using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject ZoneConstruction;
    public GameObject ZoneJeu;
    public GameObject Player1;
	public GameObject Player2;
	public Transform Player1Holder;
	public Transform Player2Holder;
	public Transform CentreConstruction;
    enum State {Construction, Exploration};
    State verifier;
    
    void Start()
    {
        ZoneJeu.SetActive(false);
        verifier = State.Construction;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (verifier == State.Construction)
            {
                ZoneJeu.SetActive(true);
                ZoneConstruction.SetActive(false);
                verifier = State.Exploration;
                Player1.transform.SetParent(Player1Holder.transform);
                Player1.transform.position = new Vector3(Player1Holder.position.x, Player1Holder.position.y, Player1Holder.position.z);
				Player2.transform.SetParent(Player2Holder.transform);
				Player2.transform.position = new Vector3(Player2Holder.position.x, Player2Holder.position.y, Player2Holder.position.z);
			}
            else if (verifier == State.Exploration)
            {
                ZoneJeu.SetActive(false);
                ZoneConstruction.SetActive(true);
                verifier = State.Construction;
                Player1.transform.SetParent(null);
                Player1.transform.position = new Vector3(CentreConstruction.position.x, CentreConstruction.position.y, CentreConstruction.position.z);
				Player2.transform.SetParent(null);
				Player2.transform.position = new Vector3(CentreConstruction.position.x, CentreConstruction.position.y, CentreConstruction.position.z);
			}
        }
    }
}
