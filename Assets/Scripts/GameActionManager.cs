using Interface;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>ゲーム内のイベントに関する制御を行うスクリプト</summary>
public class GameActionManager : MonoBehaviour
{
    [SerializeField] InputActionAsset _actions;
    [SerializeField] ConversationUI _conversationUI;
    List<GameObject> _targetList = new List<GameObject>();
    GameObject _preTarget;
    InputActionMap _player, _ui;
    StoryManager _storyManager;

    IEnumerator _eventEnumerator;

    bool _isPlaying = false;

    static GameActionManager _instance;
    public static GameActionManager Instance => _instance;

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

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        _player = _actions.FindActionMap("Player");
        _ui = _actions.FindActionMap("UI");
        ChangeActionMap();
        _storyManager = StoryManager.Instance;
        _conversationUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// アクションマップを切り替える関数
    /// </summary>
    public void ChangeActionMap()
    {
        if (_isPlaying)
        {
            _ui.Enable();
            _player.Disable();
        }
        else
        {
            _player.Enable();
            _ui.Disable();
        }
        _isPlaying = !_isPlaying;
    }

    #region アイテム関連
    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ItemSelectForKeyboard(int index, PlayerInfo player)
    {
        player.Hotbar.SelectItemForKeyboard(index);
    }

    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ItemSelectForGamepad(int index, PlayerInfo player)
    {
        player.Hotbar.SelectItemForGamepad(index);
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    public void ItemUse(PlayerInfo player)
    {
        player.Hotbar.UseItem(player);
    }

    /// <summary>
    /// アイテムの効果を発動する関数
    /// </summary>
    /// <param name="item">効果を発動するアイテム</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ItemActivate(IItemBaseEffective item, PlayerInfo player)
    {
        item.ItemBaseActivate(player);
    }

    /// <summary>
    /// プレイヤーの体力を管理する関数
    /// </summary>
    /// <param name="health">IHealthを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ChangeHealth(IHealth health, PlayerCurrentStatus player)
    {
        player.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ChangeFullness(ISaturate saturate, PlayerCurrentStatus player)
    {
        player.Saturation(saturate.Saturate);
    }
    #endregion

    #region インタラクト関連
    /// <summary>
    /// ターゲットのリストに登録する関数
    /// </summary>
    /// <param name="target">登録するターゲット</param>
    public void AddTargetList(GameObject target)
    {
        _targetList.Add(target);
    }

    /// <summary>
    /// ターゲットのリストから削除する関数
    /// </summary>
    /// <param name="target">削除するターゲット</param>
    public void RemoveTargetList(GameObject target)
    {
        _targetList.Remove(target);
    }

    /// <summary>
    /// 一番近いターゲットを返す関数
    /// </summary>
    /// <param name="position">ターゲットとの距離を測る対象</param>
    /// <returns>一番近いターゲット</returns>
    public GameObject GetTarget(Transform position)
    {
        GameObject target = null;
        foreach (GameObject go in _targetList)
        {
            if (target)
            {
                if (Vector3.SqrMagnitude(position.position - target.transform.position) > Vector3.SqrMagnitude(position.position - go.transform.position))
                {
                    target = go;
                }
            }
            else
            {
                target = go;
            }
        }

        //ターゲットの切り替わりを視覚的に変化
        if (_preTarget != target)
        {
            _preTarget?.GetComponent<EventBase>().TargetSignInactive();
            target?.GetComponent<EventBase>().TargetSignActive();
            _preTarget = target;
        }
        return target;
    }

    /// <summary>
    /// インタラクトを行う関数
    /// </summary>
    /// <param name="interact">インタラクトを行うクラス</param>
    /// <param name="player">プレイヤーの情報</param>
    /// <returns>イベントの流れ</returns>
    public void Interact(EventBase interact, PlayerInfo player)
    {
        if (_eventEnumerator == null)
        {
            Debug.Log("Event Happened");
            _eventEnumerator = interact.Event(player);
            _eventEnumerator.MoveNext();
        }
        else
        {
            Debug.Log("Already Event Happened");
        }
    }

    /// <summary>
    /// 会話の初めに行う関数
    /// </summary>
    /// <param name="interact">会話を行うクラス</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ConversationInteract(IConversationInteract interact, PlayerInfo player)
    {
        _conversationUI.gameObject.SetActive(true);
        _conversationUI.ConversationSetting(interact, player);
    }

    /// <summary>
    /// アイテムを与えるインタラクトを行う関数
    /// </summary>
    /// <param name="interact">インタラクトを行うクラス</param>
    /// <param name="player">プレイヤーの情報</param>
    public void GiveItemInteract(IGiveItemInteract interact, PlayerInfo player)
    {
        var item = interact.Item;
        if (item.ItemRole == ItemRole.KeyItem)
        {
            player.ItemList.GetItem(interact.Item);
        }
        else if (item.ItemRole == ItemRole.Food)
        {
            player.Hotbar.GetItem(interact.Item);
        }
    }

    /// <summary>
    /// エンター入力に対するアクションを行う関数
    /// </summary>
    public void PushEnterUntilTalking()
    {
        if (_storyManager.PushEnter())
        {
            //テキスト表示中

        }
        else
        {
            //テキスト表示終了
            if (_eventEnumerator != null)
            {
                //次のテキストなどを表示
                if (!_eventEnumerator.MoveNext())
                {
                    ChangeActionMap();
                    _conversationUI.gameObject.SetActive(false);
                    _eventEnumerator = null;
                }
            }
        }
    }
    #endregion
}
