using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /*
    Image clock;
    [SerializeField]
    int maxFill;
    int currentFill = 0;
    public Text timerText;
    private float startTime;
    */

    private const float SEC_PER_GAME_DAY = 480f;

    private Transform clockHourHandTransform;
    private Transform clockMinuteHandTransform;
    private float day;
    private Text timeText;
    private string ampm;

    public Material mat1;
    public Material mat2;
    public Material mat3;

    private void Awake()
    {
        clockHourHandTransform = transform.Find("hourHand");
        clockMinuteHandTransform = transform.Find("minuteHand");
        timeText = transform.Find("timeText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        var allImages = gameObject.GetComponentsInChildren<Image>();
        foreach(Image img in allImages)
        {
            if (img.name == "ClockFill")
            {
                clock = img;
                break;
            }
        }

        clock.fillAmount = 0.0f;
        startTime = Time.time;
        */
        
        day = .333f; //start at 8am
        ampm = "am";
    }

    // Update is called once per frame
    void Update()
    {
      
        day += Time.deltaTime / SEC_PER_GAME_DAY;
       
        float dayNormal = day % 1f;
        float rotDegreesPerDay = 360f;
       
        clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormal * rotDegreesPerDay *2);
        float hrsPerDay = 24f;
        clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormal * rotDegreesPerDay * hrsPerDay);
        var hrs = Mathf.Floor(dayNormal * hrsPerDay);
        string hrsString = hrs.ToString("00");

        float minPerHour = 60f;
        var min = Mathf.Floor(((dayNormal * hrsPerDay) % 1f) * minPerHour);
        string minString = min.ToString("00");
        if (hrs < 12)
        {
            
            ampm = "am";
        }
        else
        {
            ampm = "pm";
            
        }
        if (hrs > 12)
        {
            hrsString = (hrs - 12).ToString("00");
            
        }
        if (hrs == 0)
        {

            hrsString = 12.ToString("00");
        }
        if (hrs >4 && hrs < 17)
        {
            RenderSettings.skybox = mat1;
        }
        if (hrs >= 17 && hrs <20)
        {
            RenderSettings.skybox = mat2;
        }
        else if ((hrs >= 20 && hrs <=24) || (hrs >=0 && hrs <=4))
        {
            RenderSettings.skybox = mat3;
        }
        if ((min %10 ==0) || min == 0)
        {
            timeText.text = hrsString + ":" + minString + ampm;
        }
        
    }
    /*
    private void AddtoFill()
    {
        currentFill += 1;
        if (currentFill > maxFill)
        {
            currentFill = maxFill;
        }
        float fillAmount = currentFill / (float)maxFill;
        clock.fillAmount = fillAmount;
    }*/
}
