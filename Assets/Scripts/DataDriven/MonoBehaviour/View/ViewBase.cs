using UnityEngine;

namespace DataDriven
{
    public abstract class ViewBase : MonoBehaviour
    {
        public abstract void Init(UnityConnector connector);

        protected abstract void FuncRegister();

        protected abstract void FuncRemove();

        private void OnEnable()
        {
            FuncRegister();
        }

        private void OnDisable()
        {
            FuncRemove();
        }

    }
}
