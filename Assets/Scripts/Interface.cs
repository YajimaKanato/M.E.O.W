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

    /// <summary>会話のインタラクトを行うものに実装するインターフェース</summary>
    public interface ITalkInteract
    {
        /// <summary>会話を行う関数</summary>
        public void TalkInteract();
    }

    /// <summary>アイテムを獲得するインタラクトのみを行うものに実装するインターフェース</summary>
    public interface IGiveItemOnlyInteract
    {
        /// <summary>任意のアイテムを取得するプロパティ</summary>
        public ItemBase Item { get; }

        /// <summary>アイテムを与える関数</summary>
        public void GiveItemInteract();
    }
}
