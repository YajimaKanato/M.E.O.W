using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>プレイヤーの入力処理を司るクラス</summary>
    public class PlayerMono : SceneEntity
    {
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;
        static PlayerMono _instance;

        private void Awake()
        {
            //Init();
        }

        public override void Init(UnityConnector connector)
        {
            if (!_instance)
            {
                _instance = this;
                tag = TagName.PLAYER;
                _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
                _inputManager = FindFirstObjectByType<InputManager>();
                GetComponent<PlayerActionView>().Init(connector.ActionConnector);
                if (_inputManager) ActionRegister();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// アクションを登録する関数
        /// </summary>
        void ActionRegister()
        {
            //PlayScene
            _inputManager.RegisterActForStarted(_inputManager.ItemSlotActOnPlayScene, HotbarSelectForKeyboard);
            _inputManager.RegisterActForStarted(_inputManager.SlotNextActOnPlayScene, HotbarNextForGamePad);
            _inputManager.RegisterActForStarted(_inputManager.SlotBackActOnPlayScene, HotbarBackForGamePad);
            _inputManager.RegisterActForStarted(_inputManager.InteractActOnPlayScene, Interact);
            _inputManager.RegisterActForStarted(_inputManager.ItemActOnPlayScene, UseItem);
            _inputManager.RegisterActForStarted(_inputManager.DownActOnPlayScene, Down);
            _inputManager.RegisterActForStarted(_inputManager.JumpActOnPlayScene, Jump);
            _inputManager.RegisterActForStarted(_inputManager.RunActOnPlayScene, Run);
            _inputManager.RegisterActForCanceled(_inputManager.RunActOnPlayScene, Run);

            //UI
            _inputManager.RegisterActForStarted(_inputManager.EnterActOnUI, Confirm);
            _inputManager.RegisterActForStarted(_inputManager.ItemSlotActOnUI, HotbarSelectOnConversationForKeyboard);
            _inputManager.RegisterActForStarted(_inputManager.SlotNextActOnUI, HotbarNextOnConversationForGamePad);
            _inputManager.RegisterActForStarted(_inputManager.SlotBackActOnUI, HotbarBackOnConversationForGamePad);
        }

        #region PlayScene
        #region Action
        private void Update()
        {
            if (!_gameFlowManager) return;
            _gameFlowManager.Move(_inputManager.MoveActOnPlayScene.ReadValue<Vector2>(), transform.position);
        }

        /// <summary>
        /// ジャンプする関数
        /// </summary>
        void Jump(InputAction.CallbackContext context)
        {
            _gameFlowManager.Jump();
        }

        /// <summary>
        /// 足場から降りる関数
        /// </summary>
        void Down(InputAction.CallbackContext context)
        {
            _gameFlowManager.Down(context.started);
        }

        /// <summary>
        /// 走る関数
        /// </summary>
        void Run(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _gameFlowManager.Run(true);
            }
            else if (context.canceled)
            {
                _gameFlowManager.Run(false);
            }
        }
        #endregion

        /// <summary>
        /// アイテムを使用する関数
        /// Eキー/LTボタンに対応
        /// </summary>
        void UseItem(InputAction.CallbackContext context)
        {
            _gameFlowManager.UseItem();
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キーに対応
        /// </summary>
        void HotbarSelectForKeyboard(InputAction.CallbackContext context)
        {
            var key = context.control.name;
            if (key.Length > 1)
            {
                key = key.Substring(key.Length - 1);
            }
            Debug.Log(key);
            _gameFlowManager.HotbarSelectForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarNextForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarBackForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectForGamePad(IndexMove.Back);
        }
        #endregion

        #region UI
        /// <summary>
        /// インタラクトを行う関数
        /// Tキー/Aボタンに対応
        /// </summary>
        void Interact(InputAction.CallbackContext context)
        {
            _gameFlowManager.Interact();
        }

        /// <summary>
        /// 意思決定をする関数
        /// Eキー、エンターキー/Aボタンに対応
        /// </summary>
        void Confirm(InputAction.CallbackContext context)
        {
            _gameFlowManager.Confirm();
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キーに対応
        /// </summary>
        void HotbarSelectOnConversationForKeyboard(InputAction.CallbackContext context)
        {
            var key = context.control.name;
            if (key.Length > 1)
            {
                key = key.Substring(key.Length - 1);
            }
            Debug.Log(key);
            _gameFlowManager.HotbarSelectOnConversationForKeyboard(int.Parse(key) - 1);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarNextOnConversationForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectOnConversationForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        void HotbarBackOnConversationForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectOnConversationForGamePad(IndexMove.Back);
        }
        #endregion
    }
}
