using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReTimerTimeContainer : MonoBehaviour
{
    int timeCollected;
    LevelTimeMeter levelTimeMeter;
    PortalBar portalBar;
    GameObject portal;
    float percentTimeInRelationToTimeToOpenPortal = 0f;

    private void Start()
    {
        levelTimeMeter = FindObjectOfType<LevelTimeMeter>();
        portalBar = FindObjectOfType<PortalBar>();
        percentTimeInRelationToTimeToOpenPortal = 0f;
        portalBar.SetFillAmount(percentTimeInRelationToTimeToOpenPortal);
        portal = FindObjectOfType<TimePortal>().gameObject;
        portal.SetActive(false);
    }

    public int TimeCollected
    { 
        get => timeCollected; 
        set 
        {
            timeCollected = value;
            percentTimeInRelationToTimeToOpenPortal = (float) timeCollected / levelTimeMeter.timeToOpenPortal;
            portalBar.SetFillAmount(percentTimeInRelationToTimeToOpenPortal);
            //if the amount of time collected is greater or equal to the established in the LevelTimeMeter, activate portal to end level
            if (timeCollected >= levelTimeMeter.timeToOpenPortal)
            {
                portal.SetActive(true);
            }
        } 
    }
}
