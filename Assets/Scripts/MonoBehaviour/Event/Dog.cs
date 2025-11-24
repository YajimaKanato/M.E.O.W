using System.Collections;

[System.Serializable]
public class Dog : CharacterNPC
{
    public override IEnumerator Event()
    {
        return _gameManager.StatusManager.DogEvent.Event();
    }
}
