using System.Collections;
using UnityEngine;

[System.Serializable]
public class TrashCan : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.TrashCanEvent.Event();
    }
}
