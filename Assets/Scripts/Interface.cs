using Item;
using UnityEngine;

namespace Interface
{
    /// <summary>初期化処理が必要なものに実装するインターフェース</summary>
    public interface IInitialize
    {
        /// <summary>初期化関数</summary>
        /// <param name="manager">ゲームマネージャーの情報</param>
        /// <returns>初期化が完了したかどうか</returns>
        public bool Init(GameManager manager);
    }

    /// <summary>インタラクト時に停止するものに実装するインターフェース</summary>
    public interface IInteractime
    {
        /// <summary>インタラクトによる行動の制御を行う関数</summary>
        public void Interact();
    }

    /// <summary>ポーズ時に停止するものに実装するインターフェース  </summary>
    public interface IPauseTime
    {
        /// <summary>ポーズ切り替えによる行動の制御を行う関数</summary>
        public void Pause();
    }

    /// <summary>アイテムの基本のインターフェース</summary>
    public interface IItemBase
    {
        /// <summary>アイテムの説明を取得するプロパティ</summary>
        public string Info { get; }
        /// <summary>アイテムの画像を取得するプロパティ</summary>
        public Sprite Sprite { get; }
        /// <summary>アイテムの種類を取得するプロパティ</summary>
        public ItemType ItemType { get; }

        /// <summary>アイテムの役割を取得するプロパティ</summary>
        public ItemRole ItemRole { get; }
    }

    /// <summary>効果を持つアイテムの基本となるインターフェース</summary>
    public interface IItemBaseEffective
    {
        /// <summary>アイテムの基本効果を発動する関数</summary>
        public void ItemBaseActivate();
    }

    /// <summary>満腹度回復効果を持つものに実装するインターフェース</summary>
    public interface ISaturate
    {
        /// <summary>回復量を返すプロパティ</summary>
        public float Saturate { get; }
    }

    /// <summary>体力の増減効果を持つものに実装するインターフェース</summary>
    public interface IHealth
    {
        /// <summary>増減量を返すプロパティ</summary>
        public float Health { get; }
    }

    /// <summary>固有の効果を持つアイテムに実装するインターフェース</summary>
    public interface IItemUniqueEffective : IItemBaseEffective
    {
        /// <summary>アイテム固有の効果を発動する関数</summary>
        public void ItemUniqueEffective();
    }

    /// <summary>固有の効果を持つアイテムに実装するインターフェース</summary>
    public interface IItemUniqueEffective<T> : IItemBaseEffective
    {
        /// <summary>アイテム固有の効果を発動する関数</summary>
        /// <returns>任意の型</returns>
        public T ItemUniqueEffective();
    }

    /// <summary>会話を行えるものに実装するインターフェース</summary>
    public interface ITalkable
    {
        /// <summary>会話中に表示するキャラクターの名前</summary>
        public string CharacterName { get; }
        /// <summary>会話中に表示するキャラクターの画像</summary>
        public Sprite CharacterImage { get; }
    }

    /// <summary>会話のインタラクトを行うものに実装するインターフェース</summary>
    public interface IConversationInteract : ITalkable { }

    /// <summary>UIのベースインターフェース</summary>
    public interface IUIBase { }

    /// <summary>セレクトを行うものに実装するインターフェース</summary>
    public interface ISelectableUI : IUIBase
    {
        /// <summary>選択されたときの処理を行う関数</summary>
        public void SelectedSlot();
    }

    /// <summary>セレクト表示を変えるUIに実装するインターフェース</summary>
    public interface ISelectUI : IUIBase
    {
        /// <summary>選択中表示を出すかどうかを変える関数</summary>
        /// <param name="active">表示を出すかどうか</param>
        public void SelectSign(bool active);
    }

    /// <summary>プレイヤーが閉じることのできるUIに実装するインターフェース</summary>
    public interface IClosableUI : IUIBase { }

    /// <summary>エンター入力を受け付けられるUIに実装するインターフェース</summary>
    public interface IEnterUI : IUIBase
    {
        /// <summary>エンターを押したときに行う関数</summary>
        public void PushEnter();
    }

    /// <summary>ランタイム中のデータを保持するものに実装するインターフェース</summary>
    public interface IRunTime { }

}
