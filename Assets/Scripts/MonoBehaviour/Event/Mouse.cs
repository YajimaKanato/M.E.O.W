using System.Collections;
using UnityEngine;

[System.Serializable]
public class Mouse : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.MouseEvent.Event();
    }
}
