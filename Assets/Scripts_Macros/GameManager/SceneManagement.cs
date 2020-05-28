using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
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
		GameDuration = GameDuration + 3;
	}

    void Update()
    {
		ScorePlayer1 = Zone1.actualScore;
		Score1.text = ScorePlayer1.ToString();
		ScorePlayer2 = Zone2.actualScore;
		Score2.text = ScorePlayer2.ToString();
		GameDuration -= Time.deltaTime;
		TimeLeft.text = System.Math.Round(GameDuration, 0).ToString();
		if (GameDuration <= 0)
		{
			if (ScorePlayer1 > ScorePlayer2)
			{
				SceneManager.LoadScene("P1_Win");
			}
			if (ScorePlayer1 < ScorePlayer2)
			{
				SceneManager.LoadScene("P2_Win");
			}
			if (ScorePlayer1 == ScorePlayer2)
			{
				SceneManager.LoadScene("Draw_Scene");
			}
		}
	}
}
