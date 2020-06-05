using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBLevel : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [Tooltip("Name of level to be loaded when selected")][SerializeField] string levelToLoad;
    [Tooltip("Will cursor be visible in the scene?")] [SerializeField] bool cursorVisible;
    [Tooltip("Controls lock state of the mouse")] [SerializeField] CursorLockMode cursorLockMode;
    [SerializeField] GameObject playerAvatar;
    Renderer thisHUBLevelRenderer;
    Color startColor;

    void Start()
    {
        thisHUBLevelRenderer = GetComponent<Renderer>();
        startColor = thisHUBLevelRenderer.material.HasProperty("_BaseColor") ? thisHUBLevelRenderer.material.GetColor("_BaseColor") : Color.white;
        playerAvatar.SetActive(false);
    }

    private void OnMouseEnter()
    {
        thisHUBLevelRenderer.material.SetColor("_BaseColor", hoverColor);
        playerAvatar.SetActive(true);
    }

    private void OnMouseExit()
    {
        thisHUBLevelRenderer.material.SetColor("_BaseColor", startColor);
        playerAvatar.SetActive(false);
    }

    private void OnMouseDown()
    {
        LevelLoadManager.LoadLevel(levelToLoad, cursorVisible, cursorLockMode);
    }

}
