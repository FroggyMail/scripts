using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue class structure based on tutorial by Liam Sorta: http://www.liamsorta.co.uk/2017/07/30/scriptable-object-based-dialogue-system/

[System.Serializable]
public class Response
{

    public int checkPackageDeliver;
    public int checkPackageAccept;

    public string player_response;
    public Dialogue npc_response;

}