using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    int totalSeconds;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        int seconds, minutes;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            totalSeconds += 1;
            seconds = totalSeconds % 60; 
            minutes = totalSeconds / 60;
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
