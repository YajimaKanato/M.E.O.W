using Interface;
using Item;
using UnityEngine;

public abstract class ItemInfo : InitializeSO, IItemBase
{
    [SerializeField] protected ItemType _itemType;
    [SerializeField] protected ItemRole _itemRole;
    [SerializeField] protected Sprite _sprite;
    [SerializeField, TextArea] protected string _info;

    public ItemType ItemType => _itemType;
    public ItemRole ItemRole => _itemRole;
    public Sprite Sprite => _sprite;
    public string Info => _info;
    public GameManager InitManager => _gameManager;

    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }

    /// <summary>アイテムの情報を取得する関数</summary>
    /// <returns>アイテムの情報</returns>
    public virtual IItemBase ItemBase()
    {
        Debug.LogError("Please Override ItemBase!");
        return null;
    }
}

namespace Item
{
    /// <summary>アイテムの種類</summary>
    public enum ItemType
    {
        [InspectorName("肉")] Meat,
        [InspectorName("チーズ")] Cheese,
        [InspectorName("魚")] Fish,
        [InspectorName("腐った肉")] RottenMeat,
        [InspectorName("お酒")] Alcohol,
        [InspectorName("チョコ")] Chocolate,
        [InspectorName("犬の首輪")] DogCollar,
        [InspectorName("猫の首輪")] CatCollar,
        [InspectorName("倉庫のキー")] StorageKey,
        [InspectorName("ロープ")] Rope,
        [InspectorName("倉庫の地図")] StorageMap,
        [InspectorName("カメラ")] Camera,
        [InspectorName("メモリーカード")] MemoryCard,
        [InspectorName("おもちゃ")] Toy,
        [InspectorName("装置の解読コード")] DecodingCord
    }

    /// <summary>アイテムの役割</summary>
    public enum ItemRole
    {
        [InspectorName("食料")] Food,
        [InspectorName("貴重品")] KeyItem
    }
}
