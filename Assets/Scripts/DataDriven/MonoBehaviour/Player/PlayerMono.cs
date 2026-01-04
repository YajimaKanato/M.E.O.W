using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>プレイヤーの入力受付を司るクラス</summary>
    public class PlayerMono : MonoBehaviour
    {
        PlayerRuntimeData _player;
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;
        CharacterRuntime _target;
        static PlayerMono _instance;

        private void Awake()
        {
            //Init();
        }

        public void Init(PlayerRuntimeData player)
        {
            if (!_instance)
            {
                _instance = this;
                _player = player;
                _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
                _inputManager = FindFirstObjectByType<InputManager>();
                ActionRegister();
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
            _inputManager.RegisterAct(_inputManager.InteractActOnPlayScene, Interact);
            _inputManager.RegisterAct(_inputManager.EnterActOnUI, Confirm);
        }

        #region 直接Viewに関わらない入力
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
        /// キャンセルをする関数
        /// Cキー/Bボタンに対応
        /// </summary>
        public void Cancel(InputAction.CallbackContext context)
        {

        }

        /// <summary>
        /// アイテムを使用する関数
        /// Eキー/LTボタンに対応
        /// </summary>
        public void UseItem(InputAction.CallbackContext context)
        {

        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キー/LRボタンに対応
        /// </summary>
        public void HotbarSelect(InputAction.CallbackContext context)
        {

        }

        /// <summary>
        /// アイテムリストの要素を選択する関数
        /// 十字キー、WASDキー/左スティック、DPadに対応
        /// </summary>
        public void ItemListSelect(InputAction.CallbackContext context)
        {

        }
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = collision.gameObject;
            if (go.CompareTag("Character"))
            {
                if (go.TryGetComponent(out Character character))
                {
                    _target = character.CharacterRuntime;
                }
            }
        }
    }
}
