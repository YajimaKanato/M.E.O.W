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

    /// <summary>
    /// 任意の変数に対して初期化を行う関数
    /// </summary>
    /// <typeparam name="T">初期化を行う変数の型</typeparam>
    /// <param name="variable">初期化を行う変数</param>
    /// <param name="instance">初期化時に必要なインスタンス</param>
    public void InitializationForVariable<T>(out T variable, T instance) where T : class
    {
        variable = instance;
        if (variable == null) FailedInitialization();
    }

    /// <summary>
    /// 初期化が失敗したときに返す関数
    /// </summary>
    public void FailedInitialization()
    {
        Debug.Log("Initialization was Failed");
        _isInitialized = false;
    }
}
