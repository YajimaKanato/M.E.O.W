using Item;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected ItemType _itemType;
    public ItemType ItemType => _itemType;
    protected ItemRole _itemRole;
    public ItemRole ItemRole => _itemRole;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    protected virtual void Init()
    {

    }

    /// <summary>アイテムの種類を設定する関数</summary>
    protected abstract void ItemTypeSetting();
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
