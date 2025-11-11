using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class DogEvent : EventBase
{
    [SerializeField] GameObject _interactUI;
    [SerializeField] Text _text;

    Coroutine _coroutine;

    bool _isInteracting = false;

    private void Update()
    {
        if (_isInteracting)
        {
            if (_enter.triggered)
            {

            }
        }
    }

    protected override void EventSetting()
    {

    }
}
