using System.Collections;
using UnityEngine;

[System.Serializable]
public class Cat : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.CatEvent.Event();
    }
}
