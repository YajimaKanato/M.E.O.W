using UnityEngine;

namespace DataDriven
{
    /// <summary>シーン上のキャラクターのViewを司るベースクラス</summary>
    public abstract class SceneEntity : MonoBehaviour
    {
        [SerializeField] protected DataID _id;

        public DataID ID => _id;

        public abstract void Init();

        public abstract void Remove();
    }
}
