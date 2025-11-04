using UnityEngine;
using System.Collections.Generic;
using Interface;
using UnityEngine.InputSystem;

/// <summary>アクションに関する制御を行うスクリプト</summary>
public class GameActionManager : MonoBehaviour
{
    InputAction _moveAct, _jumpAct, _runAct, _interactAct, _itemAct;

    static GameActionManager _instance;
    static List<IStartTime> _iStartList;
    static List<IPauseTime> _iPauseList;
    static List<IEndTime> _iEndList;
    static List<IInteractime> _iInteractList;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        _iStartList = new List<IStartTime>();
        _iPauseList = new List<IPauseTime>();
        _iEndList = new List<IEndTime>();
        _iInteractList = new List<IInteractime>();

        //InputActionに割り当て
        _moveAct = InputSystem.actions.FindAction("Move");
        _jumpAct = InputSystem.actions.FindAction("Jump");
        _runAct = InputSystem.actions.FindAction("Run");
        _interactAct = InputSystem.actions.FindAction("Interact");
        _itemAct = InputSystem.actions.FindAction("Item");
    }

    /// <summary>
    /// 管理リストに登録する関数
    /// </summary>
    /// <typeparam name="T">任意のインターフェースを継承している型</typeparam>
    /// <param name="instance">自分自身</param>
    public static void ListRegistering<T>(T instance) where T : IStartTime, IPauseTime, IEndTime, IInteractime
    {
        if (instance is IStartTime) _iStartList.Add(instance);
        if (instance is IPauseTime) _iPauseList.Add(instance);
        if (instance is IEndTime) _iEndList.Add(instance);
        if (instance is IInteractime) _iInteractList.Add(instance);
    }

    /// <summary>
    /// 管理リストから削除する関数
    /// </summary>
    /// <typeparam name="T">任意のインターフェースを継承している型</typeparam>
    /// <param name="instance">自分自身</param>
    public static void ListDelete<T>(T instance) where T : IStartTime, IPauseTime, IEndTime, IInteractime
    {
        if (instance is IStartTime) _iStartList.Remove(instance);
        if (instance is IPauseTime) _iPauseList.Remove(instance);
        if (instance is IEndTime) _iEndList.Remove(instance);
        if (instance is IInteractime) _iInteractList.Remove(instance);
    }
}
