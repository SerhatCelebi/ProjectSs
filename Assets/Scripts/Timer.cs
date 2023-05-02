using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [SerializeField] Text timerText;

    float durationMinute, durationSecond, totalDuration;
    float currentTime;

    string timerStr = " ";

    IEnumerator UT, ST;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        UT = UpdateTimer();
        ST = StopTimerCase();
        SetDuration(0f, 0f);
    }
    public void StartTimer()
    {
        StopCoroutine(ST);
        StartCoroutine(UT);
    }
    public void StopTimer()
    {
        StopCoroutine(UT);
        StartCoroutine(ST);
    }
    public void SetDuration(float min, float sec)
    {
        durationMinute = min;
        durationSecond = sec;
        totalDuration = GetDuration();
        currentTime = totalDuration;
    }
    public float GetDuration()
    {
        return (durationMinute * 60f) + durationSecond;
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(durationSecond >= 59)
            {
                durationMinute += 1;
                durationSecond = 0;
            }
            else if(durationSecond < 59)
            {
                durationSecond += 1;
            }

            if(durationMinute >= 100)
            {
                break;
            }
            SetTimerText();
        }
    }

    void SetTimerText()
    {
        timerStr = " ";
        if(durationMinute < 10)
        {
            timerStr += "0";
        }

        timerStr += durationMinute.ToString() + ":";

        if(durationSecond < 10)
        {
            timerStr += "0";
        }

        timerStr += durationSecond.ToString();

        timerText.text = timerStr;
    }

    IEnumerator StopTimerCase()
    {
        while (true)
        {
            timerText.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            timerText.color = Color.black;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
