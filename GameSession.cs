using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //Configuration Parameters
    [Range(0.1f,10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 1;
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI livesText = null;
    [SerializeField] bool autoPlay = false;

    //State variables
    int currentScore = 0;
    int currentLives = 3;

    //Awake is called before anything else
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //Start method
    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    //Add points to score
    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void RemoveLife()
    {
        currentLives--;
        livesText.text = currentLives.ToString();
        if (currentLives > 0)
        {
            FindObjectOfType<Ball>().ResetBall();
        }
        else
        {
            FindObjectOfType<SceneLoader>().LoadGameOverScene();
        }
    }

    public bool GetAutoPlay()
    {
        return autoPlay;
    }
}
