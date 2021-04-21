using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{

    public GameObject quests;
    private Transform currentQuestTransform;
    private int currentQuest;
    private int questLength;
    private int activeQuestLength;

    // Start is called before the first frame update
    void Start()
    {
        questLength = quests.transform.childCount;
        currentQuest = 0;
        currentQuestTransform = quests.transform.GetChild(0);
    }

    public void NextQuest()
    {

        GetActiveQuestLength();
        Debug.Log("Next Button");
        Debug.Log("Quest Count: " + questLength);
        Debug.Log("Active Quest Count: " + activeQuestLength);

        //If next quest isn't last
        if(!(currentQuest + 1 >= activeQuestLength))
        {

            Transform nextQuestTransform = quests.transform.GetChild(currentQuest + 1);
            float moveDistance = nextQuestTransform.position.x - currentQuestTransform.position.x;

            quests.transform.position = new Vector3(quests.transform.position.x - moveDistance, quests.transform.position.y, quests.transform.position.z);

            currentQuestTransform = nextQuestTransform;
            currentQuest += 1;

        }
        else
        {

        }

    }

    public void PrevQuest()
    {
        GetActiveQuestLength();
        Debug.Log("Previous Button");
        Debug.Log("Quest Count: " + questLength);
        Debug.Log("Active Quest Count: " + activeQuestLength);

        //If prev quest isn't last
        if (!(currentQuest - 1 < 0))
        {

            Transform prevQuestTransform = quests.transform.GetChild(currentQuest - 1);
            float moveDistance = currentQuestTransform.position.x - prevQuestTransform.position.x;

            quests.transform.position = new Vector3(quests.transform.position.x + moveDistance, quests.transform.position.y, quests.transform.position.z);

            currentQuestTransform = prevQuestTransform;
            currentQuest -= 1;

        }
        else
        {

        }

    }

    void GetActiveQuestLength()
    {

        int activeCount = 0;

        foreach (Transform q in quests.transform)
            if (q.gameObject.activeSelf)
                activeCount++;

        activeQuestLength = activeCount;

    }

}
