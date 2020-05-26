using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    [SerializeField] float timeToWaitUntilDestruction;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToWaitUntilDestruction);
        Destroy(this.gameObject);
    }
}
