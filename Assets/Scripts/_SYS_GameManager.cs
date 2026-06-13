using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class _SYS_GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Reset,
        End
    }

    public GameState currentGameState;
    
    public static _SYS_GameManager Instance;
    public static event Action OnPriceChanged, OnLevelReset, OnLevelEnd;
    
    [HideInInspector] public int score, health, highScore, gems, price_WaterHole, price_Dash;
    
    private int _counter, _multiplier = 8;
    
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
        
        for (var i= 0; i< 2; i++) ChangePrice(i);
    }

    private void Start()
    {
        _sprites = new List<Sprite>();
        _sprites.AddRange(Resources.LoadAll<Sprite>("sprites"));

        health = 2;
        score = 0;
        
        ResumeGame();
    }
        
    public void ResetGame()
    {
        OnLevelReset?.Invoke();
        
        TXT_Health.TextUpdate(health = 2);
        TXT_Score.TextUpdate(score = 0);
        
        StartCoroutine(ResetTimer());
    }
    
    private void EndGame()
    {
        currentGameState = GameState.End;
        
        OnLevelEnd?.Invoke();
        
        TXT_HighScore.TextUpdate(highScore);

        CTR_Water.Instance.StartCoroutine(nameof(CTR_Water.ResetPosition));
    }
        
    private void ResumeGame()
    {
        currentGameState = GameState.Start;
        
        CTR_Water.Instance.Activation();
        
        CTR_Spawner.Instance.Activation();
        
        TXT_HighScore.HideScore();
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
        TXT_Score.TextUpdate(score);
        
        AddGems(i);

        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void AddGems(int count)
    {
        gems = 100;
        gems += count;
        TXT_Gem.TextUpdate(gems);
    }

    public void DecreaseGems(int count)
    {
        if (gems - count < 0) return;

        _counter++;
        gems -= count;
        TXT_Gem.TextUpdate(gems);
    }

    public void ChangePrice(int index)
    {
        // 0 price_WaterHole; 1 price_Dash
        switch (index)
        {
            case 0:
                price_WaterHole = 3 + _multiplier * _counter;
                break;
            case 1:
                price_Dash = 10;
                break;
        }
        
        OnPriceChanged?.Invoke();
    }

    public void PlayerHit(int i)
    {
        health -= i;
        TXT_Health.TextUpdate(health);
        _SYS_AudioManager.Instance.PlaySound(_SYS_AudioManager.SoundType.PlayerHurt);
        
        if (health <= 0) EndGame();
    }

    private IEnumerator ResetTimer()
    {
        currentGameState = GameState.Reset;
        
        yield return new WaitForSeconds(.1f);
        
        ResumeGame();
    }
}