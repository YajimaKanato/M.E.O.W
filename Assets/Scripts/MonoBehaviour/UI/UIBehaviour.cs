using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] protected int _id;
    protected GameManager _gameManager;
    protected RuntimeDataManager _runtimeDataManager;
    protected UIManager _uiManager;
    protected bool _isInitialized = true;
    public int ID => _id;

    public virtual bool Init(GameManager manager)
    {
        Debug.LogError("Please Override Init()!");
        return false;
    }
}
