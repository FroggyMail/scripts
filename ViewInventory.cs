using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewInventory : MonoBehaviour
{
    public GameObject inventoryGUI;
    public bool view = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject dialogue = GameObject.Find("DialogueSystem");
        if (dialogue != null && !dialogue.GetComponent<DialogueSystem>().dialogueActive)
        {

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (view == false)
                {
                    inventoryGUI.SetActive(true);
                    view = true;
                }
                else
                {
                    inventoryGUI.SetActive(false);
                    view = false;
                }

            }

        }

    }
}
