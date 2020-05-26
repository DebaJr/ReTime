using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] GameObject[] endCases;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject environment;
    [SerializeField] float fadeVelocity = 255f;
    [Tooltip("Time before showing credits")][SerializeField] float waitToCredits;

    GameObject activeEndScenario;

    // Start is called before the first frame update
    void Start()
    {
        AnalyseResult();
    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            LevelLoadManager.LoadMenu();
        }
    }

    void AnalyseResult()
    {
        int time = TimeFlow.Time;
        if (time > 900)
        {
            endCases[0].SetActive(true);
            environment.SetActive(true);
            activeEndScenario = endCases[0];
            StartCoroutine(EnableCredits());
        }
        else if (time >= 500)
        {
            endCases[1].SetActive(true);
            environment.SetActive(true);
            activeEndScenario = endCases[1];
            StartCoroutine(EnableCredits());
        }
        else if (time > Mathf.Epsilon)
        {
            endCases[2].SetActive(true);
            activeEndScenario = endCases[2];
            StartCoroutine(EnableCredits());
        }
        else
        {
            endCases[3].SetActive(true);
            activeEndScenario = endCases[3];
            StartCoroutine(EnableCredits());
        }
    }

    IEnumerator EnableCredits()
    {
        yield return new WaitForSeconds(waitToCredits);
        StartCoroutine(FadeAwayEndText());
        credits.SetActive(true);
        StartCoroutine(LoadMenuAfterCredits());
    }

    IEnumerator FadeAwayEndText()
    {
        for (int i=0; i < fadeVelocity; i++)
        {
            Color initialFrameColor = activeEndScenario.GetComponentInChildren<TextMeshProUGUI>().color;
            activeEndScenario.GetComponentInChildren<TextMeshProUGUI>().color = new Color(initialFrameColor.r, initialFrameColor.g, initialFrameColor.b, initialFrameColor.a - 1/fadeVelocity);
            yield return null;
        }
        activeEndScenario.SetActive(false);
    }

    IEnumerator LoadMenuAfterCredits()
    {
        while (credits.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        LevelLoadManager.LoadMenu();
    }
}
