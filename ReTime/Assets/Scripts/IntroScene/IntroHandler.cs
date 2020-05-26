using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroHandler : MonoBehaviour
{
    [SerializeField] Animator textAnimator;
    private void Start()
    {
        StartCoroutine(FadeSound());
    }

    public void LoadTutorial()
    {
        LevelLoadManager.LoadLevel("Tutorial", true);
    }

    IEnumerator FadeSound()
    {
        AudioSource audioSource = FindObjectOfType<AudioSource>();
        float initialVolume = audioSource.volume;
        while (textAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
        {
            audioSource.volume =  Mathf.Clamp(0.9f - textAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime, 0 , initialVolume);
            yield return null;
        }
        Destroy(audioSource.gameObject);
    }
}
