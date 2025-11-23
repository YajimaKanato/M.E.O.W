using System.Collections;
using UnityEngine;

public class Android : CharacterNPC
{
    public override IEnumerator Event(PlayerInfo player)
    {
        return _gameManager.StatusManager.AndroidEvent.Event(player);
    }
}
