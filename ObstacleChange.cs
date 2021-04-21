using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChange : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    private bool appear = true;
    public GameObject interactable;

    // Update is called once per frame

    void Update()
    {
        interactable.SetActive(appear);
    }

    public void ToggleBarrier()
    {
        appear = false;
    }

}
