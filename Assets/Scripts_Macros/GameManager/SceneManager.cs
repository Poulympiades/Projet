using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject Player1;
	public GameObject Player2;
	public Transform Player1Holder;
	public Transform Player2Holder;
	public ScoreZone Zone1;
	public int ScorePlayer1;
	public Text Score1;
	public ScoreZone Zone2;
	public int ScorePlayer2;
	public Text Score2;
	public float GameDuration;
	public Text TimeLeft;
    
    void Start()
    {
		Player1.transform.SetParent(Player1Holder.transform);
		Player1.transform.position = new Vector3(Player1Holder.position.x, Player1Holder.position.y, Player1Holder.position.z);
		Player2.transform.SetParent(Player2Holder.transform);
		Player2.transform.position = new Vector3(Player2Holder.position.x, Player2Holder.position.y, Player2Holder.position.z);
	}

    void Update()
    {
		ScorePlayer1 = Zone1.actualScore;
		Score1.text = ScorePlayer1.ToString();
		ScorePlayer2 = Zone2.actualScore;
		Score2.text = ScorePlayer2.ToString();
		GameDuration -= Time.deltaTime;
		TimeLeft.text = System.Math.Round(GameDuration, 1).ToString();
		if (GameDuration <= 0)
		{
			if (ScorePlayer1 > ScorePlayer2)
			{
				Debug.Log("Black Win");
			}
			if (ScorePlayer1 < ScorePlayer2)
			{
				Debug.Log("Red Win");
			}
			if (ScorePlayer1 == ScorePlayer2)
			{
				Debug.Log("Same Score");
			}
		}
	}
}
