using System.Collections;
using UnityEngine;

public class Mouse : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.MouseEvent.Event();
    }
}
