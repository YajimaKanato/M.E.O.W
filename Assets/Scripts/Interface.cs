using UnityEngine;

namespace Interface
{
    /// <summary>インタラクト時に停止するものに実装するインターフェース</summary>
    public interface IInteractime
    {
        public void Interact();
    }

    public interface IStartTime
    {
        public void Start();
    }

    /// <summary>ポーズ時にていしするものに実装するインターフェース  </summary>
    public interface IPauseTime
    {
        public void Pause();
    }

    public interface IEndTime
    {
        public void End();
    }
}
