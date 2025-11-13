using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] int _countdownMinutes = 10;
    [SerializeField] Text _dateTimerText;
    float _countdownSeconds;

    private void Start()
    {
        _dateTimerText = GetComponent<Text>();
        _countdownSeconds = _countdownMinutes * 60;
    }

    void Update()
    {
        if (_countdownSeconds > 0)
        {
            _countdownSeconds -= Time.deltaTime;
            if (_countdownSeconds < 0)
            {
                _countdownSeconds = 0;
            }
        }
        
        var span = new TimeSpan(0, 0, Mathf.FloorToInt(_countdownSeconds));
        _dateTimerText.text = span.ToString(@"mm\:ss");
    }
}