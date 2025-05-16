using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreText;
    public static double comboScore;
    static double multiplier;
    public static int missed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        comboScore = 0;
        multiplier = 1;
        missed = 0;
    }

    public static void Hit()
    {
        comboScore += 157 * multiplier;
        if (multiplier <= 5)
        {
            multiplier += 0.2;
        }
        Instance.hitSFX.Play();
    }
    
    public static void Miss()
    {
        multiplier = 1;
        missed += 1;
        Instance.missSFX.Play();    
    }

    public double getScore()
    {
        return comboScore;
    }

    public int getMiss()
    {
        return missed;
    }
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = comboScore.ToString();
    }
}
