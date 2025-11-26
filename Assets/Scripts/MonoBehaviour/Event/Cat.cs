using System.Collections;

public class Cat : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.DataManager.CatEvent.Event();
    }
}
