using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clues : MonoBehaviour
{

    public GameObject ClueHolder;
    public GameObject[] ClueList;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ClueList.Length; i++)
            ClueList[i].transform.parent = ClueHolder.transform;
    }

}
