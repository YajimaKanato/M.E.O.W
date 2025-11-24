using System.Collections;
using UnityEngine;

public class Android : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.AndroidEvent.Event();
    }
}
