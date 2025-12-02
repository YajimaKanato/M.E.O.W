using Interface;
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

    /// <summary>
    /// 初期化が失敗したときに返す関数
    /// </summary>
    protected void FailedInitialization()
    {
        Debug.Log("Initialization was Failed");
        _isInitialized = false;
    }
}
