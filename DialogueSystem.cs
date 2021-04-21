using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Text optionText;

    public GameObject dialogueGUI;
    public Transform dialogueBoxGUI;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.005f;

    public KeyCode DialogueInput = KeyCode.F;
    public KeyCode ExitInput = KeyCode.Escape;

    public string Names;
    private Dialogue dialogueTree;
    private GameObject[] current_packages;
    private GameObject[] current_clues;
    private AudioClip audioClip;

    public GameObject package_1;
    public GameObject package_2;
    public GameObject playerInventory;
    public GameObject logWife;
    public GameObject endBench;
    public GameObject beginningStatue; //statue gameobject when player doesnt have all logs
    public Dialogue beginningDialogue;
    public Dialogue finalDialogue; //statue gameobject when player has all logs
    public GameObject log_1;
    public GameObject log_2;
    public GameObject log_3;
    public GameObject log_4;
    public GameObject log_5;

    public string[] dialogueLines;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;
    private bool messageEnded = true;

    public float dialogCameraZoom = 0.7f;
    public float dialogCameraShift = 2.0f;
    public float dialogCameraHeight = 2.0f;
    public Camera dialogCamera;
    private Camera mainCamera;

    public Image bar1;
    public Image bar2;
    public Image bar3;
    public Image bar4;
    public Image bar5;
    public bool isDelivered;

    public int count = 0;

    AudioSource audioSource;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        dialogueText.text = "";
        bar1.enabled = false;
        bar2.enabled = false;
        bar3.enabled = false;
        bar4.enabled = false;
        bar5.enabled = false;
        log_1.SetActive(false);
        log_2.SetActive(false);
        log_3.SetActive(false);
        log_4.SetActive(false);
        log_5.SetActive(false);
        beginningStatue.GetComponent<NPC>().npcDialogue = beginningDialogue;

    }

    void Update()
    {
        if (Input.GetKeyDown(ExitInput))
        {
            DropDialogue();
            dialogueEnded = true;
            dialogueActive = false;
            StopAllCoroutines();
        }
    }

    public void setDialogueTree(Dialogue tree)
    {
        dialogueTree = tree;
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        dialogueGUI.SetActive(true);
        if (dialogueActive == true)
        {
            dialogueGUI.SetActive(false);
        }
    }

    public void NPCName()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                Debug.Log("Starting Dialogue from System");
                StartCoroutine(StartDialogue());
            }
        }

    }

    private IEnumerator StartDialogue()
    {

        if (outOfRange == false)
        {

            int messageIndex = 0;
            bool stayInLoop = true;

            dialogueText.text = "";
            optionText.text = "";

            while (stayInLoop)
            {
                if (dialogueTree.npc_messages.Length > messageIndex && stayInLoop)
                {

                    dialogueText.text = "";
                    optionText.text = "";

                    bool goodToDeliver = true;

                    string displayMessage = dialogueTree.npc_messages[messageIndex].npc_message;

                    if (dialogueTree.npc_messages[messageIndex].dialogueActions.Length > 0)
                    {
                        for (int i = 0; i < dialogueTree.npc_messages[messageIndex].dialogueActions.Length; i++)
                        {

                            if (dialogueTree.npc_messages[messageIndex].dialogueActions[i].type == Message.ActionType.AcceptPackage && current_packages.Length > 0)
                                if (current_packages[dialogueTree.npc_messages[messageIndex].dialogueActions[i].data - 1].activeSelf)
                                    markDelivered(current_packages[dialogueTree.npc_messages[messageIndex].dialogueActions[i].data - 1]);


                            if (dialogueTree.npc_messages[messageIndex].dialogueActions[i].type == Message.ActionType.DestroyBarrier)
                            {
                                GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle" + dialogueTree.npc_messages[messageIndex].dialogueActions[i].data);
                                for (int j = 0; j < obstacles.Length; j++)
                                    obstacles[j].GetComponent<ObstacleChange>().ToggleBarrier();
                            }

                            if (dialogueTree.npc_messages[messageIndex].dialogueActions[i].type == Message.ActionType.GiveClue)
                                current_clues[dialogueTree.npc_messages[messageIndex].dialogueActions[i].data - 1].SetActive(true);

                            if (dialogueTree.npc_messages[messageIndex].dialogueActions[i].type == Message.ActionType.CreateWife)
                            {
                                logWife.SetActive(true);
                                endBench.SetActive(true);
                                log_1.SetActive(false);
                                log_2.SetActive(false);
                                log_3.SetActive(false);
                                log_4.SetActive(false);
                                log_5.SetActive(false);
                            }

                            if (dialogueTree.npc_messages[messageIndex].dialogueActions[i].type == Message.ActionType.EndGame)
                            {
                                SceneManager.LoadScene(4);
                                Debug.Log("End Game!");
                            }


                        }
                    }


                    if (dialogueTree.npc_messages[messageIndex].dialogueActions.Length > 0)
                        for (int i = 0; i < dialogueTree.npc_messages[messageIndex].dialogueActions.Length; i++)
                            if (dialogueTree.npc_messages[messageIndex].dialogueActions[i].type == Message.ActionType.CheckIfDelivered)
                            {
                                goodToDeliver = false;
                                if (current_packages != null && !current_packages[dialogueTree.npc_messages[messageIndex].dialogueActions[i].data - 1].activeSelf)
                                {
                                    goodToDeliver = true;
                                }

                            }

                    if (goodToDeliver)
                    {
                        StartCoroutine(DisplayString(displayMessage));
                        messageEnded = false;
                    }
                    else
                    {
                        messageIndex += 1;
                        continue;
                    }


                    while (!messageEnded)
                        yield return 0;

                    string outRes = "";

                    if (dialogueTree.npc_messages[messageIndex].player_responses.Length == 0)
                        outRes = "Press F to Continue";

                    Response[] responses = dialogueTree.npc_messages[messageIndex].player_responses;

                    List<Response> res_list = new List<Response>();

                    for (int i = 0; i < responses.Length; i++)
                    {
                        if (responses[i].checkPackageDeliver > 0)
                        {

                            if (playerInventory.transform.GetChild(responses[i].checkPackageDeliver - 1).gameObject.activeSelf)
                            {
                                res_list.Add(responses[i]);
                            }

                        }
                        if (responses[i].checkPackageAccept > 0)
                        {

                            if (!playerInventory.transform.GetChild(responses[i].checkPackageAccept - 1).gameObject.activeSelf)
                            {
                                res_list.Add(responses[i]);
                            }

                        }

                        if (responses[i].checkPackageDeliver == 0 && responses[i].checkPackageAccept == 0)
                            res_list.Add(responses[i]);
                    }

                    responses = res_list.ToArray();

                    for (int i = 0; i < responses.Length; i++)
                    {

                        if (responses[0].player_response == "")
                        {
                            outRes = "Press F to Continue";
                            break;
                        }

                        outRes += (i + 1) + ". " + responses[i].player_response + "\n";
                    }

                    optionText.text = outRes;


                    if (dialogueTree.npc_messages[messageIndex].player_responses.Length > 0 && responses[0].player_response != "")
                        while (true)
                        {

                            if (Input.GetKeyDown(KeyCode.Alpha1) && dialogueEnded == false)
                            {
                                dialogueTree = responses[0].npc_response;
                                Debug.Log("Pressed: 1");
                                messageIndex = 0;
                                break;
                            }
                            else if (Input.GetKeyDown(KeyCode.Alpha2) && dialogueEnded == false && responses.Length > 1)
                            {
                                dialogueTree = responses[1].npc_response;
                                Debug.Log("Pressed: 2");
                                messageIndex = 0;
                                break;
                            }
                            else if (Input.GetKeyDown(KeyCode.Alpha3) && dialogueEnded == false && responses.Length > 2)
                            {
                                dialogueTree = responses[2].npc_response;
                                Debug.Log("Pressed: 3");
                                messageIndex = 0;
                                break;
                            }
                            else if (Input.GetKeyDown(KeyCode.Alpha4) && dialogueEnded == false && responses.Length > 3)
                            {
                                dialogueTree = responses[3].npc_response;
                                Debug.Log("Pressed: 4");
                                messageIndex = 0;
                                break;
                            }
                            yield return 0;
                        }
                    else if (responses.Length > 0 && responses[0].player_response == "")
                    {
                        dialogueTree = dialogueTree.npc_messages[messageIndex].player_responses[0].npc_response;
                        messageIndex = 0;
                        while (true)
                        {
                            if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                            {
                                break;
                            }
                            yield return 0;
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                            {
                                messageIndex += 1;
                                Debug.Log("New: " + messageIndex);
                                break;
                            }
                            yield return 0;
                        }

                    }

                }
                else
                {
                    stayInLoop = false;
                    break;
                }
                //yield return 0;

            }

            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }

    }

    private IEnumerator StartDialogue2()
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;

                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            messageEnded = false;
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                        //if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            messageEnded = true;
        }
    }

    public void DropDialogue()
    {
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }

    public void markDelivered(GameObject package)
    {

        package.SetActive(false);
        count++;
        switch (count)
        {
            case 1:
                bar1.enabled = true;
                log_1.SetActive(true);
                break;
            case 2:
                bar2.enabled = true;
                log_2.SetActive(true);
                break;
            case 3:
                bar3.enabled = true;
                log_3.SetActive(true);
                break;
            case 4:
                bar4.enabled = true;
                log_4.SetActive(true);
                break;
            case 5:
                bar5.enabled = true;
                log_5.SetActive(true);
                beginningStatue.GetComponent<NPC>().npcDialogue = finalDialogue;
                break;
        }

    }

    public void setPackage(GameObject[] package)
    {
        current_packages = package;
    }

    public void setClues(GameObject[] clues)
    {
        current_clues = clues;
    }

    public void setAudio(AudioClip audio)
    {
        audioClip = audio;
    }

}
