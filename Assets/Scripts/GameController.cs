using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;
    private int score;

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

	// Use this for initialization
	void Start () {
        score = 0;
        scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
