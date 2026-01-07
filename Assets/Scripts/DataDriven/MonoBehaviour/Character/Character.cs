using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のキャラクターのViewを司るベースクラス</summary>
    public abstract class Character : MonoBehaviour
    {
        protected CharacterRuntimeData _characterRuntime;
        public CharacterRuntimeData CharacterRuntime => _characterRuntime;
    }
}
