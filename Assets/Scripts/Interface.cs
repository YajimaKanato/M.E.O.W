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

    /// <summary>効果を持つアイテムの基本となるインターフェース</summary>
    public interface IItemBaseEffective
    {
        /// <summary>アイテムの基本効果を発動する関数</summary>
        /// <param name="player">プレイヤーの情報</param>
        public void ItemBaseActivate(PlayerInfo player);

        /// <summary>アイテムを使用する関数</summary>
        /// <param name="list">アイテムリスト</param>
        public void ItemUse(ItemList list);
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
    public interface IItemUniqueEffective
    {
        /// <summary>アイテム固有の効果を発動する関数</summary>
        public void ItemUniqueEffective();
    }

    /// <summary>固有の効果を持つアイテムに実装するインターフェース</summary>
    public interface IItemUniqueEffective<T>
    {
        /// <summary>アイテム固有の効果を発動する関数</summary>
        /// <returns>任意の型</returns>
        public T ItemUniqueEffective();
    }

    /// <summary>会話のインタラクトを行うものに実装するインターフェース</summary>
    public interface ITalkInteract
    {

    }

    /// <summary>アイテムを獲得するインタラクトのみを行うものに実装するインターフェース</summary>
    public interface IGiveItemInteract
    {
        /// <summary>任意のアイテムを取得するプロパティ</summary>
        public ItemBase Item { get; }
    }
}
