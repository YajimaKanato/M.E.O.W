using UnityEngine;

/// <summary>初期化処理をまとめて行うためのベースクラス</summary>
public class InitializeBehaviour : MonoBehaviour
{
    protected GameManager _gameManager;
    protected bool _isInitialized = true;
    public virtual bool Init(GameManager manager)
    {
        Debug.LogError("Please Override Init()!");
        return false;
    }
}
