using System.Collections;
using UnityEngine;

public abstract class CharacterNPC : InteractBase
{
    [SerializeField, Tooltip("ドロップアイテム")] ItemInstance _dropItem;
    [SerializeField] Vector3 _dropItemPos = new Vector3(-0.5f, -0.1f, 0);

    public override bool Init(GameManager manager)
    {
        base.Init(manager);
        if (!_dropItem)
        {
            InitializeManager.FailedInitialization();
        }
        else
        {
            if (!_dropItem.Init(manager)) InitializeManager.FailedInitialization();
            _dropItem.transform.localPosition = _dropItemPos;
            _dropItem.transform.SetParent(null);
            _dropItem.gameObject.SetActive(false);
        }

        return _isInitialized;
    }

    /// <summary>
    /// ドロップアイテムを表示する関数
    /// </summary>
    /// <param name="item">アイテムの情報</param>
    public void DropItemActive(UsableItem item)
    {
        _dropItem.ItemSetting(item);
        _dropItem.gameObject.SetActive(item != null);
    }
}
