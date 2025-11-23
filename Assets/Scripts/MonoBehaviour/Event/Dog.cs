using System.Collections;

public class Dog : CharacterNPC
{
    public override IEnumerator Event(PlayerInfo player)
    {
        return _gameManager.StatusManager.DogEvent.Event(player);
    }
}
