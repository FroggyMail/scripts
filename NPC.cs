using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public class NPC : MonoBehaviour
{

    public bool rotate = true;
    public Transform ChatBackGround;
    public Transform NPCChar;
    public GameObject[] packagesAccepted;
    public GameObject[] packagesGiven;
    public GameObject[] clues;
    public float lookSpeed = 5.0f;
    private float dialogCameraZoom;
    private float dialogCameraShift;
    private float dialogCameraHeight;
    private Camera dialogCamera;
    private Camera mainCamera;

    public DialogueSystem dialogueSystem;
    public Dialogue npcDialogue;

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;
    private bool cameraReset;

    private bool dialogueStarted = false;

    public string _name;
    private bool gotPackage = false;

    public AudioClip audioClip;

    private bool multicheck = false;

    // Start is called before the first frame update
    void Start()
    {

        mainCamera = Camera.main;
        dialogCamera = dialogueSystem.dialogCamera;

        dialogCameraZoom = dialogueSystem.dialogCameraZoom;
        dialogCameraShift = dialogueSystem.dialogCameraShift;
        dialogCameraHeight = dialogueSystem.dialogCameraHeight;


    }

    // Update is called once per frame
    void Update()
    {
       //Vector3 pos = Camera.main.WorldToScreenPoint(NPCChar.position);
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        dialogueSystem.EnterRangeOfNPC();
        if (other.gameObject.tag == "Player")
        {
            Transform npc_transform = this.gameObject.GetComponent<Transform>();
            Transform player_transform = other.gameObject.GetComponent<Transform>();

            Vector3 player_pos = player_transform.position;
            player_pos.y = npc_transform.position.y;

            if (rotate)
            {
                npc_transform.rotation = Quaternion.Slerp(npc_transform.rotation, Quaternion.LookRotation(player_pos - npc_transform.position), Time.deltaTime * lookSpeed);
            }
            else
            {
                npc_transform.rotation = npc_transform.rotation;
            }


            if (Input.GetKeyDown(KeyCode.F) && !dialogueSystem.dialogueActive && !dialogueStarted)
            {

                GameObject playerUI = GameObject.Find("PlayerUI");
                if (playerUI != null && !playerUI.GetComponent<ViewInventory>().view && !playerUI.GetComponent<ViewJournal>().view)
                {

                    dialogueStarted = true;

                    if (!dialogueSystem.dialogueActive)
                    {

                        npc_transform = this.gameObject.GetComponent<Transform>();
                        player_transform = other.gameObject.GetComponent<Transform>();

                        dialogCamera.CopyFrom(Camera.main);

                        other.gameObject.GetComponent<ThirdPersonMovement>().freezePlayer = true;
                        mainCamera.GetComponent<ThirdPersonCamera>().enabled = false;
                        Transform zoomTransform = mainCamera.GetComponent<Transform>();
                        mainCamera.enabled = false;
                        dialogCamera.enabled = true;

                        originalPlayerPosition = player_transform.position;
                        originalPlayerRotation = player_transform.rotation;
                        cameraReset = false;

                        Vector3 npc_pos = npc_transform.position;
                        npc_pos.y = player_transform.position.y;
                        player_transform.LookAt(npc_pos);
                        if (rotate)
                        {
                            npc_transform.LookAt(player_pos);
                        }

                        Transform camera_transform = dialogCamera.GetComponent<Transform>();
                        originalCameraPosition = camera_transform.position;
                        originalCameraRotation = camera_transform.rotation;

                        camera_transform.position = Vector3.Lerp(zoomTransform.position, player_transform.position, dialogCameraZoom);
                        Vector3 temp_pos = camera_transform.position;
                        camera_transform.LookAt(npc_transform.position);
                        temp_pos = camera_transform.TransformPoint(Vector3.right * dialogCameraShift);
                        temp_pos.y = npc_transform.position.y + dialogCameraHeight;
                        camera_transform.position = temp_pos;
                        camera_transform.LookAt(npc_transform.position);



                        GameObject[] ui_objs = GameObject.FindGameObjectsWithTag("PlayerUI");
                        foreach (GameObject ui_obj in ui_objs)
                            ui_obj.GetComponent<Canvas>().enabled = false;

                    }

                    this.gameObject.GetComponent<NPC>().enabled = true;

                    dialogueSystem.Names = _name;
                    Debug.Log("Setting Dialogue");
                    dialogueSystem.setDialogueTree(npcDialogue);
                    if (packagesAccepted != null)
                        dialogueSystem.setPackage(packagesAccepted);
                    if (clues != null)
                        dialogueSystem.setClues(clues);
                    if (audioClip != null)
                        dialogueSystem.setAudio(audioClip);

                    if (gotPackage == false)
                    {

                        //dialogueSystem.markDelivered(package);
                        gotPackage = true;

                    }

                    Debug.Log("Starting Dialog");
                    dialogueSystem.NPCName();

                    //dialogueSystem.isDelivered = true;
                    // FindObjectOfType<DialogueSystem>().NPCName();
                    /*if (this.gameObject.GetComponent<BoxCollider>().enabled == true)
                    {
                        FindObjectOfType<DialogueSystem>().markDelivered();
                        this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    }*/

                }

            }

            if (dialogueSystem.dialogueActive == false && dialogueStarted && !multicheck)
            {
                multicheck = true;

                dialogueStarted = false;

                if (!cameraReset)
                { 
                    dialogCamera.GetComponent<Transform>().position = originalCameraPosition;
                    dialogCamera.GetComponent<Transform>().rotation = originalCameraRotation;
                    other.gameObject.GetComponent<Transform>().position = originalPlayerPosition;
                    other.gameObject.GetComponent<Transform>().rotation = originalPlayerRotation;

                    dialogCamera.enabled = false;
                    mainCamera.enabled = true;

                    cameraReset = true;
                }

                other.gameObject.GetComponent<ThirdPersonMovement>().freezePlayer = false;
                mainCamera.GetComponent<ThirdPersonCamera>().enabled = true;

                GameObject[] ui_objs = GameObject.FindGameObjectsWithTag("PlayerUI");
                foreach (GameObject ui_obj in ui_objs)
                    ui_obj.GetComponent<Canvas>().enabled = true;

                multicheck = false;
            }

        }
    }

    public void OnTriggerExit(Collider other)
    {
        //gotPackage = true;
        dialogueSystem.OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
