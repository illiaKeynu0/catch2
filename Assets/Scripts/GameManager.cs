using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Reset,
        End
    }

    public GameState currentGameState;
    
    public static GameManager Instance;
    
    public int score, health, highScore;
    
    private List<Sprite> _sprites;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _sprites = new List<Sprite>();
        _sprites.AddRange(Resources.LoadAll<Sprite>("sprites"));

        health = 2;
        score = 0;
        
        ResumeGame();
    }

    private void Update()
    {
        if (health <= 0)
        {
            EndGame();
        }
    }

    public void ResetGame()
    {
        if (PlayerController.Instance.ReplayPressed)
        {
            PlayerController.Instance.ReplayPressed = false;
            
            health = 2;
            score = 0;

            StartCoroutine(ResetTimer());
        }
    }
    
    private void ResumeGame()
    {
        currentGameState = GameState.Start;
    }
    
    private void EndGame()
    {
        currentGameState = GameState.End;
        
        if (score > highScore)
        {
            highScore = score;
        }

        ResetGame();
    }

    public Sprite RandomSprite()
    {
        return _sprites[Random.Range(0, _sprites.Count)];
    }

    public int RandomIndex(int objectsCount)
    {
        var randomObject = Random.Range(-2, objectsCount);

        return randomObject;
    }

    public void AddScore()
    {
        score++;
    }

    public void Hit()
    {
        health--;
    }

    private IEnumerator ResetTimer()
    {
        currentGameState = GameState.Reset;

        yield return new WaitForSeconds(.1f);
        
        ResumeGame();
    }
}