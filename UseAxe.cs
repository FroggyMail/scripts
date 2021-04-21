using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAxe : MonoBehaviour
{
    public GameObject melted1;
    public GameObject melted2;
    public GameObject melted3;
    public bool hasAxe = false;
    public GameObject iceAxe;
    public GameObject controlText;
    public GameObject axeUI;
    // Start is called before the first frame update
    void Start()
    {
        melted1.SetActive(false);
        melted2.SetActive(false);
        melted3.SetActive(false);
        axeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "IceAxe")
        {
            hasAxe = true;
            iceAxe.SetActive(false);
            axeUI.SetActive(true);
        }
       
        
    }

    void OnTriggerStay(Collider other)
    {
        if (hasAxe == true && (other.gameObject.tag == "Iceblock1" || other.gameObject.tag == "Iceblock2" || other.gameObject.tag == "Iceblock3"))
        {
            controlText.SetActive(true);
        }
        if (other.gameObject.tag == "Iceblock1" && hasAxe == true && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("iceblock 1");
            Destroy(other.gameObject);
            melted1.SetActive(true);
            StartCoroutine(Wait(melted1));
        }
        if (other.gameObject.tag == "Iceblock2" && hasAxe == true && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("iceblock 2");
            Destroy(other.gameObject);
            melted2.SetActive(true);
            StartCoroutine(Wait(melted2));
        }
        if (other.gameObject.tag == "Iceblock3" && hasAxe == true && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("iceblock 3");
            Destroy(other.gameObject);
            melted3.SetActive(true);
            StartCoroutine(Wait(melted3));
        }
    }

    void OnTriggerExit(Collider other)
    {
        controlText.SetActive(false);
    }

    IEnumerator Wait(GameObject melted)
    {
        controlText.SetActive(false);
        yield return new WaitForSeconds(15);
        melted.SetActive(false);

    }
}
