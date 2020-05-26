using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScoreViewer : MonoBehaviour
{
    ReTimerTimeContainer timeContainer;
    TextMeshProUGUI debugScore;
    void Start()
    {
        debugScore = GetComponent<TextMeshProUGUI>();
        timeContainer = FindObjectOfType<ReTimerTimeContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        debugScore.text = timeContainer.TimeCollected.ToString();
    }
}
