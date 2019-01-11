using System;
using UnityEngine;

public class TanksMathf
{
    public static float RoundDown(float value)
    {
        if (value - Math.Truncate(value) == 0.5f)
        {
            return Mathf.Floor(value);
        }
        else
        {
            return Mathf.Round(value);
        }     
    }
}

