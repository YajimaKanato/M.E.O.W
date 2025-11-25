using Interface;
using UnityEngine;

/// <summary>初期化処理をまとめて行うためのベースクラス</summary>
[System.Serializable]
public class InitializeBehaviour : MonoBehaviour, IInitialize
{
    protected GameManager _gameManager;
    public virtual bool Init(GameManager manager)
    {
        Debug.LogError("Please Override Init()!");
        return false;
    }
}
