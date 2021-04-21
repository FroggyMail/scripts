using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue class structure based on tutorial by Liam Sorta: http://www.liamsorta.co.uk/2017/07/30/scriptable-object-based-dialogue-system/

[System.Serializable]
public class Message
{

    [System.Serializable]
    public enum ActionType
    { AcceptPackage, GiveClue, GivePackage, CheckIfDelivered, DestroyBarrier, CreateWife, EndGame };

    [System.Serializable]
    public class Action
    {
        public ActionType type;
        public int data;
    };

    public Action[] dialogueActions;

    public string npc_message;
    public Response[] player_responses;

}
