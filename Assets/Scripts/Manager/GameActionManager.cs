using ActionMap;
using Interface;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゲーム内のアクションに関する制御を行うクラス</summary>
public class GameActionManager : InitializeBehaviour
{
    DataManager _dataManager;
    UIManager _uiManager;
    PlayerInputActionManager _playerInputActionManager;
    HotbarRunTime _hotbarRunTime;
    PlayerRunTimeOnPlayScene _playerRunTimeOnPlayScene;
    MessageRunTime _messageRunTime;
    MenuRunTime _menuRunTime;
    List<CharacterNPC> _targetList;
    CharacterNPC _preTarget;
    CharacterNPC _target;

    IEnumerator _eventEnumerator;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _dataManager, _gameManager.DataManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        InitializeManager.InitializationForVariable(out _playerInputActionManager, _gameManager.PlayerInputActionManager);
        InitializeManager.InitializationForVariable(out _hotbarRunTime, _dataManager.HotbarRunTime);
        InitializeManager.InitializationForVariable(out _playerRunTimeOnPlayScene, _dataManager.PlayerRunTimeOnPlayScene);
        InitializeManager.InitializationForVariable(out _messageRunTime, _dataManager.MessageRunTime);
        InitializeManager.InitializationForVariable(out _menuRunTime, _dataManager.MenuRunTime);
        InitializeManager.InitializationForVariable(out _targetList, new List<CharacterNPC>());

        return _isInitialized;
    }

    #region アイテム関連
    /// <summary>
    /// アイテムを受け取る関数
    /// </summary>
    /// <param name="item">アイテム</param>
    public void GetItem(ItemInfo item)
    {
        if (item.ItemRole == ItemRole.KeyItem)
        {
            _uiManager.GetKeyItem(item);
            Debug.Log($"Get => {item}");
        }
        else if (item.ItemRole == ItemRole.Food)
        {
            var index = _hotbarRunTime.GetItem((UsableItem)item);
            if (index != -1)
            {
                _uiManager.SlotUpdate((UsableItem)item, index);
                Debug.Log($"Get => {item}");
            }
            else
            {

            }
        }
    }

    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    public void ItemSelectForKeyboard(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUI>())
        {
            _hotbarRunTime.SelectItemForKeyboard(index);
            _uiManager.Select<ISelectableNumberUI>();
        }
        else
        {
            Debug.Log("Invaild Command");
        }
    }

    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void ItemSelectForGamepad(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUI>())
        {
            _hotbarRunTime.SelectItemForGamepad(index);
            _uiManager.Select<ISelectableNumberUI>();
        }
        else
        {
            Debug.Log("Invaild Command");
        }
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    public void ItemUse()
    {
        var item = _hotbarRunTime.UseItem();
        if (item != null)
        {
            item.ItemBaseActivate();
            _uiManager.SlotUpdate(null);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// プレイヤーの体力を管理する関数
    /// </summary>
    /// <param name="health">IHealthを実装したスクリプトのインスタンス</param>
    public void ChangeHealth(IHealth health)
    {
        _playerRunTimeOnPlayScene.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    public void ChangeFullness(ISaturate saturate)
    {
        _playerRunTimeOnPlayScene.Saturation(saturate.Saturate);
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
    /// <returns>イベントの流れ</returns>
    public void Interact()
    {
        if (!_target)
        {
            Debug.Log("Not Event");
            return;
        }

        if (_eventEnumerator == null)
        {
            _eventEnumerator = _target.Event();
            if (_eventEnumerator == null) return;
            Debug.Log("Event Happened");
            _playerInputActionManager.ChangeActionMap(ActionMapName.UI);
            _eventEnumerator.MoveNext();
        }
        else
        {
            Debug.Log("Already Event Happened");
        }
    }

    /// <summary>
    /// エンター入力に対するアクションを行う関数
    /// </summary>
    public void PushEnter()
    {
        if (_uiManager.ActionCheck<IEnterUI>())
        {
            _uiManager.PushEnter();
            //イベント発生中
            if (_eventEnumerator != null)
            {
                if (!_messageRunTime.IsTyping)
                {
                    if (!_eventEnumerator.MoveNext())
                    {
                        _playerInputActionManager.ChangeActionMap(ActionMapName.Player);
                        _eventEnumerator = null;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Invaild Command");
        }
    }
    #endregion

    #region UI関連
    /// <summary>
    /// メニューを開く関数
    /// </summary>
    public void OpenMenu()
    {
        if (_uiManager.OpenMenu())
        {
            _playerInputActionManager.ChangeActionMap(ActionMapName.UI);
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// メニューを選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    public void MenuSelectForKeyboard(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUI>())
        {
            _menuRunTime.SelectMenuForKeyboard(index);
            _uiManager.Select<ISelectableNumberUI>();
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// メニューを選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void MenuSelectForGamepad(int index)
    {
        if (_uiManager.ActionCheck<ISelectableNumberUI>())
        {
            _menuRunTime.SelectMenuForGamepad(index);
            _uiManager.Select<ISelectableNumberUI>();
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }

    /// <summary>
    /// UIを閉じる関数
    /// </summary>
    public void CloseUI()
    {
        if (_uiManager.CloseUI())
        {
            _playerInputActionManager.ChangeActionMap();
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }
    #endregion
}
