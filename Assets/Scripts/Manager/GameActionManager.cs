using Interface;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>ゲーム内のイベントに関する制御を行うスクリプト</summary>
public class GameActionManager : InitializeBehaviour
{
    [SerializeField] InputActionAsset _actions;
    List<CharacterNPC> _targetList = new List<CharacterNPC>();
    CharacterNPC _preTarget;
    CharacterNPC _target;
    InputActionMap _player, _ui;

    IEnumerator _eventEnumerator;

    bool _isPlaying = false;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override void Init(GameManager manager)
    {
        _player = _actions.FindActionMap("Player");
        _ui = _actions.FindActionMap("UI");
        ChangeActionMap();
        _gameManager = manager;
        Debug.Log($"{this} has Initialized");
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
        _gameManager.StatusManager.PlayerRunTime.SelectItemForKeyboard(index);
        _gameManager.Hotbar.SelectedSlot();
    }

    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ItemSelectForGamepad(int index, PlayerInfo player)
    {
        _gameManager.StatusManager.PlayerRunTime.SelectItemForGamepad(index);
        _gameManager.Hotbar.SelectedSlot();
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    public void ItemUse(PlayerInfo player)
    {
        //_gameManager.Hotbar.UseItem(player);
        var item = _gameManager.StatusManager.PlayerRunTime.UseItem();
        if (item != null)
        {
            item.ItemBaseActivate(player);
            _gameManager.Hotbar.SlotUpdate(item);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
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
    public void ChangeHealth(IHealth health, PlayerInfo player)
    {
        _gameManager.StatusManager.PlayerRunTime.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤーの情報</param>
    public void ChangeFullness(ISaturate saturate, PlayerInfo player)
    {
        _gameManager.StatusManager.PlayerRunTime.Saturation(saturate.Saturate);
    }
    #endregion

    #region インタラクト関連
    /// <summary>
    /// ターゲットのリストに登録する関数
    /// </summary>
    /// <param name="target">登録するターゲット</param>
    public void AddTargetList(CharacterNPC target)
    {
        _targetList.Add(target);
    }

    /// <summary>
    /// ターゲットのリストから削除する関数
    /// </summary>
    /// <param name="target">削除するターゲット</param>
    public void RemoveTargetList(CharacterNPC target)
    {
        _targetList.Remove(target);
    }

    /// <summary>
    /// 一番近いターゲットを返す関数
    /// </summary>
    /// <param name="position">ターゲットとの距離を測る対象</param>
    public void GetTarget(Transform position)
    {
        _target = null;
        foreach (CharacterNPC go in _targetList)
        {
            if (_target)
            {
                if (Vector3.SqrMagnitude(position.position - _target.transform.position) > Vector3.SqrMagnitude(position.position - go.transform.position))
                {
                    _target = go;
                }
            }
            else
            {
                _target = go;
            }
        }

        //ターゲットの切り替わりを視覚的に変化
        if (_preTarget != _target)
        {
            _preTarget?.TargetSignInactive();
            _target?.TargetSignActive();
            _preTarget = _target;
        }
    }

    /// <summary>
    /// インタラクトを行う関数
    /// </summary>
    /// <param name="player">プレイヤーの情報</param>
    /// <returns>イベントの流れ</returns>
    public void Interact(PlayerInfo player)
    {
        if (!_target)
        {
            Debug.Log("Not Event");
            return;
        }

        ChangeActionMap();
        if (_eventEnumerator == null)
        {
            _eventEnumerator = _target.Event(player);
            if (_eventEnumerator == null) return;
            Debug.Log("Event Happened");
            _eventEnumerator.MoveNext();
        }
        else
        {
            Debug.Log("Already Event Happened");
        }
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
            _gameManager.ItemList?.GetItem(interact.Item);
            Debug.Log($"Get => {item}");
        }
        else if (item.ItemRole == ItemRole.Food)
        {
            //_gameManager.Hotbar?.GetItem(interact.Item);
            if (_gameManager.StatusManager.PlayerRunTime.GetItem(interact.Item))
            {
                _gameManager.Hotbar.SlotUpdate((IItemBaseEffective)item);
                Debug.Log($"Get => {item}");
            }
            else
            {

            }
        }
    }

    /// <summary>
    /// エンター入力に対するアクションを行う関数
    /// </summary>
    public void PushEnterUntilTalking()
    {
        if (_gameManager.InteractUIManager.PushEnter())
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
                    _gameManager.InteractUIManager.ConversationEnd();
                    _eventEnumerator = null;
                }
            }
        }
    }
    #endregion
}
