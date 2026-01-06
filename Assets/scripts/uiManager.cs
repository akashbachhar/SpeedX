using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{
    public Button[] buttons;
    public Text scoreText;
    int score;
    public Text highScoreText;
    bool gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOver = false;
        InvokeRepeating("UpdateScore", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    void UpdateScore()
    {
        if (!gameOver)
        {
            score++;
        }
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    
    public void GameOver()
    {
        gameOver = true;
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
        highScoreText.gameObject.SetActive(true);
    }

    public void Play(){
       SceneManager.LoadScene("level1");
    }

    public void Pause(){
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void Menu(){
        SceneManager.LoadScene("menuScene");
    }

    public void Quit(){
        Application.Quit();
    }
}