using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のキャラクターのViewを司るベースクラス</summary>
    public abstract class Character : MonoBehaviour
    {
        protected CharacterRuntime _characterRuntime;
        public CharacterRuntime CharacterRuntime => _characterRuntime;
        public abstract void Init(CharacterRuntime character);
    }
}
