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
        _countdownSeconds = _countdownMinutes * 60;
        UpdateTimerDisplay();
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
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        var span = new TimeSpan(0, 0, Mathf.CeilToInt(_countdownSeconds));
        _dateTimerText.text = span.ToString(@"mm\:ss");
    }
}