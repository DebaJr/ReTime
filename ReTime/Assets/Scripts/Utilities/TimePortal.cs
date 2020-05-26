using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePortal : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    LevelTimeMeter levelTimeMeter;
    // Start is called before the first frame update
    void Start()
    {
        levelTimeMeter = FindObjectOfType<LevelTimeMeter>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(playerTag))
        {
            levelTimeMeter.EndLevel();
        }    
    }
}
