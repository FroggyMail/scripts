using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewJournal : MonoBehaviour
{
    public GameObject clueGUI;
    public bool view = false;
    public Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            GameObject dialogue = GameObject.Find("DialogueSystem");
            if (dialogue != null && !dialogue.GetComponent<DialogueSystem>().dialogueActive)
            {

                if (view == false)
                {

                    clueGUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    view = true;

                    GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
                    player.GetComponent<ThirdPersonMovement>().freezePlayer = true;

                    mainCam.GetComponent<ThirdPersonCamera>().enabled = false;


                }
                else
                {
                    clueGUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    view = false;
                    mainCam.GetComponent<ThirdPersonCamera>().enabled = true;
                    GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
                    player.GetComponent<ThirdPersonMovement>().freezePlayer = false;
                }

            }

        }
    }
}
