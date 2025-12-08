using System.Collections;
using UnityEngine;

/// <summary>イベントのベースクラス</summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterNPC : InitializeBehaviour
{
    [SerializeField, Tooltip("インタラクト対象になったときの表示オブジェクト")] GameObject _targetSign;
    [SerializeField, Tooltip("ドロップアイテム")] ItemInstance _dropItem;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _objectManager, _gameManager.ObjectManager);

        if (tag != "Event")
        {
            tag = "Event";
        }

        if (gameObject.layer != LayerMask.NameToLayer("Event"))
        {
            gameObject.layer = LayerMask.NameToLayer("Event");
        }

        if (!_targetSign)
        {
            InitializeManager.FailedInitialization();
        }
        else
        {
            TargetSignInactive();
        }

        GetComponent<Rigidbody2D>().gravityScale = 0;

        return _isInitialized;
    }

    /// <summary>
    /// 任意のイベントの情報を返す関数
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator Event();

    /// <summary>
    /// インタラクト対象表示をアクティブにする関数
    /// </summary>
    public void TargetSignActive()
    {
        _targetSign.SetActive(true);
    }

    /// <summary>
    /// インタラクト対象表示を非アクティブにする関数
    /// </summary>
    public void TargetSignInactive()
    {
        _targetSign.SetActive(false);
    }

    /// <summary>
    /// ドロップアイテムを表示する関数
    /// </summary>
    /// <param name="item">アイテムの情報</param>
    public void DropItemActive(ItemInfo item)
    {
        _dropItem.gameObject.SetActive(true);
        _dropItem.ItemImageSetting(item.Sprite);
    }
}
