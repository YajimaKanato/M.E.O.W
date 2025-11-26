using System.Collections;

public class Mouse : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.DataManager.MouseEvent.Event();
    }
}
