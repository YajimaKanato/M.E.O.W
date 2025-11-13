using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] int countdownMinutes = 10;
    [SerializeField] Text dateTimerText;
    float countdownSeconds;

    private void Start()
    {
        dateTimerText = GetComponent<Text>();
        countdownSeconds = countdownMinutes * 60;
    }

    void Update()
    {
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            if (countdownSeconds < 0)
            {
                countdownSeconds = 0;
            }
        }

        var span = new TimeSpan(0, 0, Mathf.FloorToInt(countdownSeconds));
        dateTimerText.text = span.ToString(@"mm\:ss");
    }
}