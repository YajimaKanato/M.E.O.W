using UnityEngine;

namespace DataDriven
{
    /// <summary>データのID</summary>
    public enum DataID
    {
        Hotbar,
        ItemList,
        Menu,
        Config,
        ItemCollection,
        Log,
        Info,
        BGM,
        SE,
        Text,
        Player,
        Dog,
        Cat,
        Mouse,
        Android
    }

    /// <summary>エンターキーを押したときの入力タイプ</summary>
    public enum EnterType
    {
        Interact,
        SpecificItem,
        AnyItem
    }

    /// <summary>アクションマップの名前</summary>
    public enum ActionMapName
    {
        [InspectorName("プレイ中")] Player,
        [InspectorName("UI")] UI,
        [InspectorName("タイトル")] OutGame,
        [InspectorName("メニュー")] Menu,
        [InspectorName("該当なし")] Unknown
    }

    /// <summary>イベントの種類のラベル</summary>
    public enum EventType
    {
        [InspectorName("会話")] Talk,
        [InspectorName("アイテムをあげる")] GiveItem,
        [InspectorName("特定のアイテムが条件")] SpecificItem,
        [InspectorName("任意のアイテムが条件")] AnyItem,
        [InspectorName("次に進む")] Next,
        [InspectorName("繰り返す")] Loop
    }

    /// <summary>話し手の名前</summary>
    public enum TalkerName
    {
        [InspectorName("主人公")] Player,
        [InspectorName("犬")] Dog,
        [InspectorName("野良猫")] Cat,
        [InspectorName("ネズミ")] Mouse,
        [InspectorName("アンドロイド")] Android,
        [InspectorName("なし")] Unknown
    }

    /// <summary>アイテムの種類</summary>
    public enum ItemType
    {
        [InspectorName("体に良い食べ物")] GoodFood,
        [InspectorName("体に悪い食べ物")] BadFood,
        [InspectorName("キーアイテム")] KeyItem
    }

    /// <summary>アイテムの名前</summary>
    public enum ItemName
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

    /// <summary>設定のラベル</summary>
    public enum ConfigType
    {
        [InspectorName("BGM")] BGM,
        [InspectorName("SE")] SE,
        [InspectorName("テキスト表示速度")] TextSpeed
    }

    /// <summary>メニューの状態を表すラベル</summary>
    public enum MenuType
    {
        [InspectorName("設定")] Config,
        [InspectorName("アイテムリスト")] ItemList,
        [InspectorName("会話ログ")] Log,
        [InspectorName("操作説明")] Info
    }

    /// <summary>配列選択の方向をラベル化</summary>
    public enum IndexMove
    {
        Next = 1,
        Back = -1
    }

    /// <summary>スロット選択の方向をラベル化</summary>
    public enum SlotMoveHorizontal
    {
        RIGHT = 1,
        LEFT = -1
    }

    /// <summary>スロット選択の方向をラベル化</summary>
    public enum SlotMoveVertical
    {
        DOWN = 1,
        UP = -1
    }
}
