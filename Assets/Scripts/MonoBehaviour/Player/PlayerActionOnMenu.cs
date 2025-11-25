
public class PlayerActionOnMenu : InitializeBehaviour
{
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) return false;
        return true;
    }

    public void ChangeItemSlot()
    {

    }
}
