using System.Collections;

public class Dog : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.DogEvent.Event();
    }
}
