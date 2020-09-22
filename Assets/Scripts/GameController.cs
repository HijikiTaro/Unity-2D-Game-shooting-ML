using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameoverText;
    public Text scoreText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameoverText.SetActive(false);
        scoreText.text = "SCORE:" + score;
    }

    public void AddScore()
    {
        score += 100;
        scoreText.text = "SCORE:" + score;
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "SCORE:" + score;
    }

    public void GameOver()
    {
        gameoverText.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(gameoverText.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main");
            }

        }

    }
}
