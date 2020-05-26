using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    
    [SerializeField] string playerTag = "Player";
    LevelTimeMeter levelTimeMeter;
    ReTimerTimeContainer timeContainer;
    
     private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Die();
        }
    }

    private void Die()
    {
        levelTimeMeter = FindObjectOfType<LevelTimeMeter>();
        timeContainer = FindObjectOfType<ReTimerTimeContainer>();
        timeContainer.TimeCollected = 0;
        levelTimeMeter.SetAllTimeLostTextActive();
        levelTimeMeter.EndLevel();
    }
}
