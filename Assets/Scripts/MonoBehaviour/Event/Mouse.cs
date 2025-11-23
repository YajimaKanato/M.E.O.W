using System.Collections;
using UnityEngine;

public class Mouse : CharacterNPC
{
    public override IEnumerator Event(PlayerInfo player)
    {
        return _gameManager.StatusManager.MouseEvent.Event(player);
    }
}
