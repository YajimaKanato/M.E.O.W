using UnityEngine;

/// <summary>ScriptableObjectにするもののベースクラス</summary>
public abstract class InitializeSO : ScriptableObject
{
    protected GameManager _gameManager;
    protected bool _isInitialized = true;

    public abstract bool Init(GameManager manager);
}
