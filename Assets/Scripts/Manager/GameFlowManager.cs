using UnityEngine;
using System.Collections.Generic;
using Interface;

/// <summary>アクションに関する制御を行うスクリプト</summary>
[System.Serializable]
public class GameFlowManager : InitializeBehaviour
{
    List<IPauseTime> _iPauseList;
    List<IInteractime> _iInteractList;
    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) return false;

        _iPauseList = new List<IPauseTime>();
        if (_iPauseList == null) return false;

        _iInteractList = new List<IInteractime>();
        if (_iInteractList == null) return false;
        return true;
    }

    /// <summary>
    /// 管理リストに登録する関数
    /// </summary>
    /// <typeparam name="T">任意のインターフェースを継承している型</typeparam>
    /// <param name="instance">自分自身</param>
    public void ListRegistering<T>(T instance) where T : IPauseTime, IInteractime
    {
        if (instance is IPauseTime) _iPauseList.Add(instance);
        if (instance is IInteractime) _iInteractList.Add(instance);
    }

    /// <summary>
    /// 管理リストから削除する関数
    /// </summary>
    /// <typeparam name="T">任意のインターフェースを継承している型</typeparam>
    /// <param name="instance">自分自身</param>
    public void ListDelete<T>(T instance) where T : IPauseTime, IInteractime
    {
        if (instance is IPauseTime) _iPauseList.Remove(instance);
        if (instance is IInteractime) _iInteractList.Remove(instance);
    }
}
