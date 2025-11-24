using System.Collections;
using UnityEngine;

public class TrashCan : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.TrashCanEvent.Event();
    }
}
