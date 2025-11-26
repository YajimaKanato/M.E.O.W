using Interface;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゲーム内のイベントに関する制御を行うスクリプト</summary>
public class GameActionManager : InitializeBehaviour
{
    List<CharacterNPC> _targetList = new List<CharacterNPC>();
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
        return _isInitialized;
    }

    #region アイテム関連
    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選んだスロットの番号</param>
    public void ItemSelectForKeyboard(int index)
    {
        _gameManager.DataManager.PlayerRunTime.SelectItemForKeyboard(index);
        _gameManager.InteractUIManager.SelectedSlot();
    }

    /// <summary>
    /// アイテムを選ぶ関数
    /// </summary>
    /// <param name="index">選ぶスロットの方向</param>
    public void ItemSelectForGamepad(int index)
    {
        _gameManager.DataManager.PlayerRunTime.SelectItemForGamepad(index);
        _gameManager.InteractUIManager.SelectedSlot();
    }

    /// <summary>
    /// アイテムを使用する関数
    /// </summary>
    public void ItemUse()
    {
        //_gameManager.Hotbar.UseItem(player);
        var item = _gameManager.DataManager.PlayerRunTime.UseItem();
        if (item != null)
        {
            item.ItemBaseActivate();
            _gameManager.InteractUIManager.SlotUpdate(item);
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
    public void ItemActivate(IItemBaseEffective item)
    {
        item.ItemBaseActivate();
    }

    /// <summary>
    /// プレイヤーの体力を管理する関数
    /// </summary>
    /// <param name="health">IHealthを実装したスクリプトのインスタンス</param>
    public void ChangeHealth(IHealth health)
    {
        _gameManager.DataManager.PlayerRunTime.ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    public void ChangeFullness(ISaturate saturate)
    {
        _gameManager.DataManager.PlayerRunTime.Saturation(saturate.Saturate);
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
            _gameManager.PlayerInputActionManager.ChangeActionMap();
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
    public void GiveItemInteract(IGiveItemInteract interact)
    {
        var item = interact.Item;
        if (item.ItemRole == ItemRole.KeyItem)
        {
            _gameManager.InteractUIManager.GetKeyItem(interact.Item);
            Debug.Log($"Get => {item}");
        }
        else if (item.ItemRole == ItemRole.Food)
        {
            if (_gameManager.DataManager.PlayerRunTime.GetItem(interact.Item))
            {
                _gameManager.InteractUIManager.SlotUpdate((IItemBaseEffective)item);
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
                    _gameManager.PlayerInputActionManager.ChangeActionMap();
                    _gameManager.InteractUIManager.ConversationEnd();
                    _eventEnumerator = null;
                }
            }
        }
    }
    #endregion
}
