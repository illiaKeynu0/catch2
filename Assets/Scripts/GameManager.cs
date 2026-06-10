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
    
    private void ResumeGame()
    {
        currentGameState = GameState.Start;
        
        WaterLayer.Instance.Activation();
        
        SpawnerController.Instance.Activation();
        
        HighScoreText.HideScore();
    }
    
    private void EndGame()
    {
        currentGameState = GameState.End;
        
        HighScoreText.TextUpdate(highScore);

        WaterLayer.Instance.StartCoroutine(nameof(WaterLayer.ResetPosition));
    }
    
    public void ResetGame()
    {
        health = 2;
        score = 0;

        StartCoroutine(ResetTimer());
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

    public void AddScore(int i)
    {
        score += i;
        ScoreText.TextUpdate(score);
        
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void PlayerHit(int i)
    {
        health -= i;
        HealthText.TextUpdate(health);
        AudioManager.Instance.PlaySound(AudioManager.SoundType.PlayerHurt);
        
        if (health <= 0) EndGame();
    }

    private IEnumerator ResetTimer()
    {
        currentGameState = GameState.Reset;
        
        yield return new WaitForSeconds(.1f);
        
        ResumeGame();
    }
}