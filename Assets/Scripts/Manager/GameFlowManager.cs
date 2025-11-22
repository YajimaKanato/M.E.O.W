using UnityEngine;
using System.Collections.Generic;
using Interface;
using UnityEngine.InputSystem;

/// <summary>アクションに関する制御を行うスクリプト</summary>
public class GameFlowManager : MonoBehaviour, IInitialize
{
    GameManager _gameManager;
    List<IPauseTime> _iPauseList;
    List<IInteractime> _iInteractList;
    /// <summary>
    /// 初期化関数
    /// </summary>
    public void Init(GameManager manager)
    {
        _iPauseList = new List<IPauseTime>();
        _iInteractList = new List<IInteractime>();
        _gameManager = manager;
        Debug.Log($"{this} has Initialized");

        ////InputActionに割り当て
        //_moveAct = InputSystem.actions.FindAction("Move");
        //_jumpAct = InputSystem.actions.FindAction("Jump");
        //_runAct = InputSystem.actions.FindAction("Run");
        //_interactAct = InputSystem.actions.FindAction("Interact");
        //_itemAct = InputSystem.actions.FindAction("Item");
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
    public void ListDelete<T>(T instance) where T :  IPauseTime, IInteractime
    {
        if (instance is IPauseTime) _iPauseList.Remove(instance);
        if (instance is IInteractime) _iInteractList.Remove(instance);
    }
}
