using System.Collections;
using UnityEngine;

public class Dog : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.DataManager.DogEvent.Event();
    }
}
