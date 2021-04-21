using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    public GameObject clue1;
    public GameObject clue2;
    
      public GameObject clue3;
        public GameObject clue4;
    // Start is called before the first frame update
    void Start()
    {
        clue1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Clue")
        {
            Destroy(other.gameObject);
            clue1.SetActive(true);
        }
        if (other.gameObject.tag == "Clue2")
        {
            Destroy(other.gameObject);
            clue2.SetActive(true);
        }                
        if (other.gameObject.tag == "Clue3")
        {
            Destroy(other.gameObject);
            clue3.SetActive(true);
        }                
        if (other.gameObject.tag == "Clue4")
        {
            Destroy(other.gameObject);
            clue4.SetActive(true);
        }
    }
}