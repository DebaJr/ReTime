using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimeMeter : MonoBehaviour
{
    [Header("UI Configuration variables")]
    [Tooltip("Exact name of the level UI Object containing the animator")][SerializeField] string levelUIName = "LevelUI";
    [Tooltip("Exact name of the level UI animator trigger")][SerializeField] string levelUIAnimatorTrigger = "EndLevel";
    [Tooltip("Start level animation duration to be removed from spent time to compensate for 'unplayable' time after level loaded")][SerializeField] float startLevelAnimationLenght = 1.5f;
    [Tooltip("Button from TimeOverall object to lead to HUB after level is over")][SerializeField] GameObject nextLevelButton;
    
    [Header("Level/gameplay configuration variables")]
    [Tooltip("Exact name of the ReTimer tag (default is 'Player')")][SerializeField] string playerTag = "Player";
    [Tooltip("Difficulty factor. Default is 0,5 -> 60seconds * 0,5 = 30 'Time' spent")]
    [Range(0,3)][SerializeField] float dificultyLevel = 0.5f;
    [Tooltip("Amount of time collected needed to open portal")]
    [Range(0,1000)] public int timeToOpenPortal = 200;
    
    [Header("UI text objects references")]
    [Tooltip("Text field for time collected value from TimeOverall object")][SerializeField] TextMeshProUGUI timeCollectedText;
    [Tooltip("Text field for time spent value from TimeOverall object")][SerializeField] TextMeshProUGUI timeSpentText;
    [Tooltip("Text field for total time value from TimeOverall object")][SerializeField] TextMeshProUGUI totalTimeText;
    [Tooltip("Object to be activated when Die is called by a Hazard")][SerializeField] GameObject allTimeLostText;

    [Header("Level unlocking config")]
    [Tooltip("Number of the next level to be unlocked (ex.: Level01 unlocks level '2')")][SerializeField] int nextLevelToUnlock;

    //private vars not shown in inspector
    Animator levelUIAnimations;
    int timeCollected;
    int timeSpent;
    
    private void Awake() {
        levelUIAnimations = GameObject.Find(levelUIName).GetComponent<Animator>();
        allTimeLostText.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }

    //expose LevelEnd functionalities to other scripts
    public void EndLevel(bool died)
    {
        OnLevelEnd(died);
    }

    void OnLevelEnd(bool died)
    {
        levelUIAnimations.SetTrigger(levelUIAnimatorTrigger);
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        timeCollected = player.GetComponent<ReTimerTimeContainer>().TimeCollected;
        timeSpent = Mathf.FloorToInt((Time.timeSinceLevelLoad - startLevelAnimationLenght) * dificultyLevel);
        //disable player
        player.GetComponent<CharacterMovement>().enabled = false;
        player.GetComponent<CharacterRotator>().enabled = false;
        player.GetComponent<CharacterJump>().enabled = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(CountCollected(timeCollected, timeSpent, died));
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Sum the total time collected on the level with a crescent text
    IEnumerator CountCollected(int _timeCollected, int _timeSpent, bool _died)
    {
        int amount = 0;
        timeCollectedText.text = amount.ToString();
        for (int i = 0; i <= _timeCollected; i++)
        {
            amount = i;
            timeCollectedText.text = amount.ToString();
            if (Input.GetButton("Jump"))
            {
                timeCollectedText.text = _timeCollected.ToString();
                break;
            }
            yield return null;
        }
        StartCoroutine(CountSpent(_timeCollected, _timeSpent, _died));
    }

    //Sum the total time spent on the level with a crescent text
    IEnumerator CountSpent(int _timeCollected, int _timeSpent, bool _died)
    {
        int amount = 0;
        timeSpentText.text = amount.ToString();
        for (int i = 0; i <= _timeSpent; i++)
        {
            amount = i;
            timeSpentText.text = amount.ToString();
            if (Input.GetButton("Jump"))
            {
                timeSpentText.text = _timeSpent.ToString();
                break;
            }
            yield return null;
        }
        StartCoroutine(SumTotalLevelTime(_timeCollected, _timeSpent, _died));
    }

    //Sum the total time of the level with a crescent text
    IEnumerator SumTotalLevelTime(int _timeCollected, int _timeSpent, bool _died)
    {
        yield return new WaitForSeconds(0.5f);
        //TODO play sound!
        totalTimeText.text = (_timeCollected - _timeSpent).ToString();
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            TimeFlow.Time += (_timeCollected - _timeSpent);
            if(PlayerPrefs.GetInt("LevelsUnlocked") < nextLevelToUnlock && !_died)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", nextLevelToUnlock);
            }
        }
        nextLevelButton.SetActive(true);
    }

    public void SetAllTimeLostTextActive()
    {
        allTimeLostText.SetActive(true);
    }

    //Expose LevelLoader to UI Button
    public void GoToHUB()
    {
        LevelLoadManager.LoadHUB();
    }
}
