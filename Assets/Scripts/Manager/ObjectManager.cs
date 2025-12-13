using Interface;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>シーン上のオブジェクトに関する制御を行うクラス</summary>
public class ObjectManager : InitializeBehaviour
{
    [Serializable]
    class InitializeObject
    {
        [SerializeField, Tooltip("シーン上のオブジェクト")] InitializeBehaviour _obj;
        [SerializeField] bool _active = true;

        public InitializeBehaviour Obj => _obj;
        public bool Active => _active;
    }

    [SerializeField] ItemDataList _itemDataList;
    [SerializeField] EventDataList _eventDataList;
    [Header("Initialize Object")]
    [SerializeField] InitializeObject[] _initObj;
    List<InteractBase> _targetList;
    InteractBase _preTarget;
    InteractBase _target;
    public InteractBase Target => _target;

    static ObjectManager _instance;

    public override bool Init(GameManager manager)
    {
        if (_instance == null)
        {
            _instance = this;
            _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
            _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
            _isInitialized = InitializeManager.InitializationForVariable(out _targetList, new List<InteractBase>());
            //アイテムの初期化
            if (!_itemDataList || !_itemDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
            //イベントデータの初期化
            if (!_eventDataList || !_eventDataList.Init(manager)) _isInitialized = InitializeManager.FailedInitialization();
        }

        foreach (var initObj in _initObj)
        {
            if (initObj.Obj.Init(_gameManager))
            {
                initObj.Obj.gameObject.SetActive(initObj.Active);
            }
            else
            {
                _isInitialized = InitializeManager.FailedInitialization();
            }
        }
        return _isInitialized;
    }

    /// <summary>
    /// アイテムを交換する関数
    /// </summary>
    /// <param name="item">交換するアイテム</param>
    public void ItemChange(UsableItem item)
    {
        if (_target is CharacterNPC)
        {
            ((CharacterNPC)_target).DropItemActive(item);
        }
        else if (_target is ItemInstance)
        {
            ((ItemInstance)_target).ItemSetting(item);
        }
    }

    /// <summary>
    /// ドロップアイテムを獲得する関数
    /// </summary>
    /// <param name="item">アイテム</param>
    public void GetDropItem(UsableItem item)
    {
        _runtimeDataManager.GetData<ItemInstanceRunTime>(_target.ID).ItemDataSetting(item);
        _target.gameObject.SetActive(false);
    }

    /// <summary>
    /// プレイヤーの体力を管理する関数
    /// </summary>
    /// <param name="health">IHealthを実装したスクリプトのインスタンス</param>
    /// <param name="id">使用したキャラクターのID</param>
    public void ChangeHealth(IHealth health, int id)
    {
        _runtimeDataManager.GetData<PlayerRunTimeOnPlayScene>(id).ChangeHP(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISatuateを実装したスクリプトのインスタンス</param>
    /// <param name="id">使用したキャラクターのID</param>
    public void ChangeFullness(ISaturate saturate, int id)
    {
        _runtimeDataManager.GetData<PlayerRunTimeOnPlayScene>(id).Saturation(saturate.Saturate);
    }

    /// <summary>
    /// ターゲットのリストに登録する関数
    /// </summary>
    /// <param name="target">登録するターゲット</param>
    public void AddTargetList(InteractBase target)
    {
        _targetList.Add(target);
    }

    /// <summary>
    /// ターゲットのリストから削除する関数
    /// </summary>
    /// <param name="target">削除するターゲット</param>
    public void RemoveTargetList(InteractBase target)
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
        foreach (InteractBase go in _targetList)
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
}
