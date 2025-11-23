using System.Collections;
using UnityEngine;

public class TrashCan : CharacterNPC
{
    public override IEnumerator Event(PlayerInfo player)
    {
        return _gameManager.StatusManager.TrashCanEvent.Event(player);
    }
}
