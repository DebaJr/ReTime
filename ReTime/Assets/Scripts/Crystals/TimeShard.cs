using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShard : MonoBehaviour
{
    [Tooltip("Value to be added to the TimeFlow at the end of the level")][SerializeField] int timeValue = 1;
    [Tooltip("Mark this to random pick a crystal material at Start")][SerializeField] bool wantRandomMaterial;
    [Tooltip("Array of references to the different crystal materials")][SerializeField] Material[] randomMaterial;
    AudioSource audioSource;
    float audioClipLength;
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioClipLength = audioSource.clip.length + 0.1f;
        if (wantRandomMaterial)
        {
            int index = Random.Range(0, randomMaterial.Length);
            GetComponentInChildren<Renderer>().material = randomMaterial[index];
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReTimerTimeContainer player = other.GetComponent<ReTimerTimeContainer>();
            player.TimeCollected += timeValue;
            audioSource.Play();
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            StartCoroutine(SelfDestruct());
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(audioClipLength); 
        Destroy(gameObject);
    }
}