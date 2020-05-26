using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] crystals;
    [SerializeField] GameObject crystalsText;
    bool shardsEnded = false;
    bool crystalsEnded = false;

    private void Start()
    {
        foreach(GameObject crystal in crystals)
        {
            crystal.SetActive(false);
        }
        crystalsText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!shardsEnded)
        {
            TimeShard[] countingShards = FindObjectsOfType<TimeShard>();
            if (countingShards.Length <= Mathf.Epsilon)
            {
                foreach (GameObject crystal in crystals)
                {
                    crystal.SetActive(true);
                }
                crystalsText.SetActive(true);
                shardsEnded = true;
            }
        }

        if(!crystalsEnded && shardsEnded)
        {
            TimeCrystal[] countingCrystals = FindObjectsOfType<TimeCrystal>();
            Debug.Log(countingCrystals.Length);
            if (countingCrystals.Length <= Mathf.Epsilon)
            {
                crystalsEnded = true;
                crystalsText.SetActive(false);
            }
        }
    }


}
