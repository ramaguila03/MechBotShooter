using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile
{
    public static float GetHighestScore()
    {
        return PlayerPrefs.GetFloat("HighestScore", 0);
    }

    public static void SetHighestScore(float p_value)
    {
        PlayerPrefs.SetFloat("HighestScore", p_value);
    }
    
    public static float GetPoints()
    {
        return PlayerPrefs.GetFloat("Points", 0);
    }

    public static void AddPoints(float p_value)
    {
        float currentValue = GetPoints();
        currentValue += p_value;
        PlayerPrefs.SetFloat("Points", currentValue);
    }
    
    public static void SubtractPoints(float p_value)
    {
        float currentValue = GetPoints();
        currentValue -= p_value;
        PlayerPrefs.SetFloat("Points", currentValue);
    }
    
    public static float GetShootingSpeed()
    {
        return PlayerPrefs.GetFloat("ShootingSpeed", 8.5f);
    }

    public static void SetShootingPower(float p_value)
    {
        PlayerPrefs.SetFloat("ShootingSpeed", p_value);
    }
    
    public static int GetShootingPower()
    {
        return PlayerPrefs.GetInt("ShootingPower", 55);
    }

    public static void SetShootingSpeed(int p_value)
    {
        PlayerPrefs.SetInt("ShootingPower", p_value);
    }
    
    

}
