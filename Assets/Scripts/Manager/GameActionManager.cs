using ActionMap;
using Interface;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゲーム内のアクションに関する制御を行うスクリプト</summary>
public class GameActionManager : InitializeBehaviour
{
    List<CharacterNPC> _targetList;
    CharacterNPC _preTarget;
    CharacterNPC _target;

    IEnumerator _eventEnumerator;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();

        _targetList = new List<CharacterNPC>();
        if (_targetList == null) FailedInitialization();

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
            _gameManager.UIManager.GetKeyItem(item);
            Debug.Log($"Get => {item}");
        }
        else if (item.ItemRole == ItemRole.Food)
        {
            var index = _gameManager.DataManager.HotbarRunTime.GetItem((UsableItem)item);
            if (index != -1)
            {
                _gameManager.UIManager.SlotUpdate((UsableItem)item, index);
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
        if (_gameManager.UIManager.ActionCheck<ISelectableNumberUI>())
        {
            _gameManager.DataManager.HotbarRunTime.SelectItemForKeyboard(index);
            _gameManager.UIManager.Select<ISelectableNumberUI>();
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
        if (_gameManager.UIManager.ActionCheck<ISelectableNumberUI>())
        {
            _gameManager.DataManager.HotbarRunTime.SelectItemForGamepad(index);
            _gameManager.UIManager.Select<ISelectableNumberUI>();
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
        var item = _gameManager.DataManager.HotbarRunTime.UseItem();
        if (item != null)
        {
            item.ItemBaseActivate();
            _gameManager.UIManager.SlotUpdate(null);
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
        _gameManager.DataManager.PlayerRunTimeOnPlayScene.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    public void ChangeFullness(ISaturate saturate)
    {
        _gameManager.DataManager.PlayerRunTimeOnPlayScene.Saturation(saturate.Saturate);
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
            _gameManager.PlayerInputActionManager.ChangeActionMap(ActionMapName.UI);
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
        if (_gameManager.UIManager.ActionCheck<IEnterUI>())
        {
            _gameManager.UIManager.PushEnter();
            //イベント発生中
            if (_eventEnumerator != null)
            {
                if (!_gameManager.DataManager.MessageRunTime.IsTyping)
                {
                    if (!_eventEnumerator.MoveNext())
                    {
                        _gameManager.PlayerInputActionManager.ChangeActionMap(ActionMapName.Player);
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
        if (_gameManager.UIManager.OpenMenu())
        {
            _gameManager.PlayerInputActionManager.ChangeActionMap(ActionMapName.UI);
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
        if (_gameManager.UIManager.ActionCheck<ISelectableNumberUI>())
        {
            _gameManager.DataManager.MenuRunTime.SelectMenuForKeyboard(index);
            _gameManager.UIManager.Select<ISelectableNumberUI>();
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
        if (_gameManager.UIManager.ActionCheck<ISelectableNumberUI>())
        {
            _gameManager.DataManager.MenuRunTime.SelectMenuForGamepad(index);
            _gameManager.UIManager.Select<ISelectableNumberUI>();
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
        if (_gameManager.UIManager.CloseUI())
        {
            _gameManager.PlayerInputActionManager.ChangeActionMap();
        }
        else
        {
            Debug.Log("Invalid Command");
        }
    }
    #endregion
}
