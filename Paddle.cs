using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] float xMin = 1f;
    [SerializeField] float xMax = 15f;
    [SerializeField] float screenWidthInUnits = 16f;

    //Cached references
    GameSession gameSession;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePosition = new Vector2 (transform.position.x,transform.position.y);
        paddlePosition.x = GetXPos();
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if(gameSession.GetAutoPlay())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Mathf.Clamp(Input.mousePosition.x / Screen.width * screenWidthInUnits, xMin, xMax);
        }
    }
}
