using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeFlow
{
    private static int time;

    public static int Time { 
        get => time;
        set 
        {
            time = value;
            time = Mathf.Clamp(time, 0, 1000);
            PlayerPrefs.SetInt("TimeFlow", time);
        }
    }
}
