using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextQuest : MonoBehaviour
{

    public GameObject quests;
    private int questLength;
    private int activeQuestLength;

    // Start is called before the first frame update
    void Start()
    {
        questLength = quests.transform.childCount;
    }

    public void RunOnClick()
    {

        GetActiveQuestLength();
        Debug.Log("Quest Count: " + questLength);
        Debug.Log("Active Quest Count: " + activeQuestLength);


        
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
