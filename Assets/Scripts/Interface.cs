using Item;
using UnityEngine;

namespace Interface
{
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
        /// <param name="id">使用したキャラクターのID</param>
        public void ItemBaseActivate(int id);
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

    /// <summary>会話を行えるものに実装するインターフェース</summary>
    public interface ITalkable
    {
        /// <summary>会話中に表示するキャラクターの名前</summary>
        public string CharacterName { get; }
        /// <summary>会話中に表示するキャラクターの画像</summary>
        public Sprite CharacterImage { get; }
    }

    /// <summary>UIのベースインターフェース</summary>
    public interface IUIBase { }

    /// <summary>セレクトを行うものに実装するインターフェース</summary>
    public interface ISelectableUI : IUIBase { }

    /// <summary>セレクトを行うものに実装するインターフェース</summary>
    public interface ISelectableVerticalArrowUI : ISelectableUI
    {

        /// <summary>選択されたときの処理を行う関数</summary>
        /// <param name="index">切り替えるインデックス</param>
        public void SelectedCategory(int index);
    }

    /// <summary>横方向の入力で選択切り替えを行うものに実装するインターフェース</summary>
    public interface ISelectableHorizontalArrowUI : ISelectableUI
    {

        /// <summary>選択されたときの処理を行う関数</summary>
        /// <param name="index">切り替えるインデックス</param>
        public void SelectedCategory(int index);
    }

    /// <summary>番号で選択切り替えを行うものに実装するインターフェース</summary>
    public interface ISelectableNumberUIForKeyboard : ISelectableUI
    {

        /// <summary>選択されたときの処理を行う関数</summary>
        /// <param name="index">切り替えるインデックス</param>
        public void SelectedCategory(int index);
    }

    /// <summary>番号で選択切り替えを行うものに実装するインターフェース</summary>
    public interface ISelectableNumberUIForGamepad : ISelectableUI
    {

        /// <summary>選択されたときの処理を行う関数</summary>
        /// <param name="index">切り替えるインデックス</param>
        public void SelectedCategory(int index);
    }

    /// <summary>開閉機能を持つUIに実装するインターフェース</summary>
    public interface IUIOpenAndClose : IUIBase
    {
        /// <summary>UIを開く関数</summary>
        public void OpenSetting();

        /// <summary>UIを閉じる関数</summary>
        public void Close();
    }

    /// <summary>プレイヤーが閉じることのできるUIに実装するインターフェース</summary>
    public interface IClosableUI : IUIOpenAndClose { }

    /// <summary>エンター入力を受け付けられるUIに実装するインターフェース</summary>
    public interface IEnterUI : IUIBase
    {
        /// <summary>エンターを押したときに行う関数</summary>
        public void PushEnter();
    }

    /// <summary>ランタイム中のデータを保持するものに実装するインターフェース</summary>
    public interface IRunTime { }

}
