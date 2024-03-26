using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring : MonoBehaviour
{
    private int score;

    public int Score
    {
        get; 
    }
    public void addScore(int score)
    {
		this.score += score;
	}
        
}
