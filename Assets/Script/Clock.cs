using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    const float degreesPerHour =  30f,
                degreesPerMinute = 6f,
                degreesPerSecond = 6f;
    public Transform hoursTransform, minutesTransform, secondsTransform;
    public bool continuous;
    

    private void Awake()
    {
        // Debug.Log("Time right now: "+System.DateTime.Now);
        System.DateTime time = System.DateTime.Now;
        hoursTransform.localRotation = Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f,time.Second * degreesPerSecond, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (continuous)
        {
            UpdateContinuous();

        } else
        {
            UpdateDiscrete();
        }
    }

    void UpdateDiscrete()
    {
        System.DateTime time = System.DateTime.Now;
        hoursTransform.localRotation = Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
    }

    void UpdateContinuous()
    {
        System.TimeSpan time = System.DateTime.Now.TimeOfDay;
        hoursTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
    }


}
