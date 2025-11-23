using System.Collections;
using UnityEngine;

public class Cat : CharacterNPC
{
    public override IEnumerator Event(PlayerInfo player)
    {
        return _gameManager.StatusManager.CatEvent.Event(player);
    }
}
