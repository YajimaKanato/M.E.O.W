using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>プレイヤーの入力処理を司るクラス</summary>
    public class PlayerMono : MonoBehaviour, IMono
    {
        [SerializeField] DataID _id = DataID.Player;
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;
        DataID _target;
        static PlayerMono _instance;

        public DataID ID => _id;

        private void Awake()
        {
            //Init();
        }

        public void Init()
        {
            if (!_instance)
            {
                _instance = this;
                _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
                _inputManager = FindFirstObjectByType<InputManager>();
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
            _inputManager.RegisterAct(_inputManager.ItemSlotActOnPlayScene, HotbarSelectForKeyboard);
            _inputManager.RegisterAct(_inputManager.SlotNextActOnPlayScene, HotbarNextForGamePad);
            _inputManager.RegisterAct(_inputManager.SlotBackActOnPlayScene, HotbarBackForGamePad);
            _inputManager.RegisterAct(_inputManager.InteractActOnPlayScene, Interact);
            _inputManager.RegisterAct(_inputManager.ItemActOnPlayScene, UseItem);

            //UI
            _inputManager.RegisterAct(_inputManager.EnterActOnUI, Confirm);
            _inputManager.RegisterAct(_inputManager.ItemSlotActOnUI, HotbarSelectOnConversationForKeyboard);
            _inputManager.RegisterAct(_inputManager.SlotNextActOnUI, HotbarNextOnConversationForGamePad);
            _inputManager.RegisterAct(_inputManager.SlotBackActOnUI, HotbarBackOnConversationForGamePad);
        }

        #region PlayScene
        /// <summary>
        /// アイテムを使用する関数
        /// Eキー/LTボタンに対応
        /// </summary>
        public void UseItem(InputAction.CallbackContext context)
        {
            _gameFlowManager.UseItem();
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キーに対応
        /// </summary>
        public void HotbarSelectForKeyboard(InputAction.CallbackContext context)
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
        public void HotbarNextForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        public void HotbarBackForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectForGamePad(IndexMove.Back);
        }
        #endregion

        #region UI
        /// <summary>
        /// インタラクトを行う関数
        /// Tキー/Aボタンに対応
        /// </summary>
        public void Interact(InputAction.CallbackContext context)
        {
            _gameFlowManager.Interact(_target);
        }

        /// <summary>
        /// 意思決定をする関数
        /// Eキー、エンターキー/Aボタンに対応
        /// </summary>
        public void Confirm(InputAction.CallbackContext context)
        {
            _gameFlowManager.Confirm();
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キーに対応
        /// </summary>
        public void HotbarSelectOnConversationForKeyboard(InputAction.CallbackContext context)
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
        public void HotbarNextOnConversationForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectOnConversationForGamePad(IndexMove.Next);
        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// LRボタンに対応
        /// </summary>
        public void HotbarBackOnConversationForGamePad(InputAction.CallbackContext context)
        {
            _gameFlowManager.HotbarselectOnConversationForGamePad(IndexMove.Back);
        }
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = collision.gameObject;
            if (go.CompareTag("Character"))
            {
                if (go.TryGetComponent(out SceneEntity character))
                {
                    _target = character.ID;
                }
            }
        }
    }
}
