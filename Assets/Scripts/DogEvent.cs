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
        _event = new Action[] { };
    }

    void GiveFoodEvent()
    {

    }

    IEnumerator GiveFoodCroutine()
    {
        _isInteracting = true;

        _interactUI.SetActive(true);
        _text.text = "";
        foreach (var s in "‚ß‚µ‚­‚ê")
        {
            _text.text += s;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        _interactUI.SetActive(false);
        yield break;
    }
}
