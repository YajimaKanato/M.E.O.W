using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    protected GameManager _gameManager;
    protected bool _isInitialized = true;
    public virtual bool Init(GameManager manager)
    {
        Debug.LogError("Please Override Init()!");
        return false;
    }
}
