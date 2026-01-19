using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>入力受付するベースクラス</summary>
    public abstract class InputBase : MonoBehaviour
    {
        [SerializeField] protected InputActionAsset _actions;
        [SerializeField] protected ActionMapName _actionMapName;
        protected InputActionMap _actionMap;
        InputDevice _preDevice;

        public ActionMapName ActionMapName => _actionMapName;

        public abstract void ActionMapSetting();

        public void ActionMapEnable()
        {
            _actionMap.Enable();
        }

        public void ActionMapDisable()
        {
            _actionMap.Disable();
        }

        /// <summary>
        /// InputActionに関数を登録する関数
        /// </summary>
        /// <param name="act">関数を登録するInputAction</param>
        /// <param name="context">登録する関数</param>
        public void RegisterActForStarted(InputAction act, Action<InputAction.CallbackContext> context)
        {
            act.started += context;
        }

        /// <summary>
        /// InputActionに関数を登録する関数
        /// </summary>
        /// <param name="act">関数を登録するInputAction</param>
        /// <param name="context">登録する関数</param>
        public void RegisterActForPerformed(InputAction act, Action<InputAction.CallbackContext> context)
        {
            act.performed += context;
        }

        /// <summary>
        /// InputActionに関数を登録する関数
        /// </summary>
        /// <param name="act">関数を登録するInputAction</param>
        /// <param name="context">登録する関数</param>
        public void RegisterActForCanceled(InputAction act, Action<InputAction.CallbackContext> context)
        {
            act.canceled += context;
        }

        /// <summary>
        /// InputActionに関数を登録する関数
        /// </summary>
        /// <param name="act">関数を登録するInputAction</param>
        /// <param name="context">登録する関数</param>
        public void UnRegisterActForStarted(InputAction act, Action<InputAction.CallbackContext> context)
        {
            act.started -= context;
        }

        /// <summary>
        /// InputActionに関数を登録する関数
        /// </summary>
        /// <param name="act">関数を登録するInputAction</param>
        /// <param name="context">登録する関数</param>
        public void UnRegisterActForPerformed(InputAction act, Action<InputAction.CallbackContext> context)
        {
            act.performed -= context;
        }

        /// <summary>
        /// InputActionに関数を登録する関数
        /// </summary>
        /// <param name="act">関数を登録するInputAction</param>
        /// <param name="context">登録する関数</param>
        public void UnRegisterActForCanceled(InputAction act, Action<InputAction.CallbackContext> context)
        {
            act.canceled -= context;
        }
    }
}
