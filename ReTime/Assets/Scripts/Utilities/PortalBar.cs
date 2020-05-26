using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalBar : MonoBehaviour
{
    [SerializeField] Image portalFill;

    public void SetFillAmount(float amount)
    {
        portalFill.fillAmount = amount;
    }
}
