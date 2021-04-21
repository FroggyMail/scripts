using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue class structure based on tutorial by Liam Sorta: http://www.liamsorta.co.uk/2017/07/30/scriptable-object-based-dialogue-system/

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue / New Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{

    public Message[] npc_messages;

}
