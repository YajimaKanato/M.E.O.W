using UnityEngine;

/// <summary>マネージャーのベースクラス</summary>
public abstract class ManagerBase : MonoBehaviour
{
    protected GameManager _gameManager;
    protected bool _isInitialized = true;

    public abstract bool Init(GameManager manager);
}
