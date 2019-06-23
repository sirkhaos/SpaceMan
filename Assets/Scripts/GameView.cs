using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText, maxScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.ShareInstans.currentGameState == GameState.inGame)
        {
            int coints = GameManager.ShareInstans.collectedObject;
            float Score = 0;
            float MaxScore = 0;

            coinsText.text = coints.ToString("00000");
            scoreText.text = "Score: "+Score.ToString("0000000.0");
            maxScoreText.text = "TopScore: " + MaxScore.ToString("0000000.0");
        }
    }
}
