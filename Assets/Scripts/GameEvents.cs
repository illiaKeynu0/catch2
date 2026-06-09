using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameEvents : ScriptableObject
{
    private static List<Sprite> sprites;
    private static int score, health = 3;

    public static Sprite RandomSprite()
    {
        sprites = new List<Sprite>();
        
        sprites.AddRange(Resources.LoadAll<Sprite>("sprites"));
        
        return sprites[Random.Range(0, sprites.Count)];
    }

    public static void AddScore()
    {
        score++;
    }

    public static void Hit()
    {
        health--;
    }
}