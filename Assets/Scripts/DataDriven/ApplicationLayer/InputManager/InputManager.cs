using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>入力受付を司るクラス</summary>
    public class InputManager : MonoBehaviour
    {
        [SerializeField, Tooltip("アクションアセット")] InputActionAsset _actions;
        InputDevice _preDevice;
        InputActionMap _player, _ui, _outGameCategory, _menu, _outGame;
        static InputManager _instance;

        #region InputAction
        //プレイ中
        InputAction _moveActOnPlayScene;
        InputAction _downActOnPlayScene;
        InputAction _runActOnPlayScene;
        InputAction _jumpActOnPlayScene;
        InputAction _interactActOnPlayScene;
        InputAction _itemActOnPlayScene;
        InputAction _itemSlotActOnPlayScene;
        InputAction _slotNextActOnPlayScene;
        InputAction _slotBackActOnPlayScene;
        InputAction _menuActOnPlayScene;
        //UI
        InputAction _menuActOnUI;
        InputAction _menuSelectActOnUI;
        InputAction _itemSlotActOnUI;
        InputAction _slotNextActOnUI;
        InputAction _slotBackActOnUI;
        InputAction _enterActOnUI;
        InputAction _cancelActOnUI;
        InputAction _selectUpOnUI;
        InputAction _selectDownOnUI;
        InputAction _selectRightOnUI;
        InputAction _selectLeftOnUI;
        //アウトゲーム
        InputAction _enterActOnOutGame;
        InputAction _selectUpOnOutGame;
        InputAction _selectDownOnOutGame;
        //アウトゲームカテゴリー
        InputAction _menuNextActOnOutGameCategory;
        InputAction _menuBackActOnOutGameCategory;
        InputAction _menuSelectActOnOutGameCategory;
        InputAction _enterActOnOutGameCategory;
        InputAction _cancelActOnOutGameCategory;
        InputAction _selectUpOnOutGameCategory;
        InputAction _selectDownOnOutGameCategory;
        InputAction _selectRightOnOutGameCategory;
        InputAction _selectLeftOnOutGameCategory;
        //メニュー
        InputAction _menuSelectActOnMenu;
        InputAction _slotNextActOnMenu;
        InputAction _slotBackActOnMenu;
        InputAction _selectUpActOnMenu;
        InputAction _selectDownActOnMenu;
        InputAction _selectRightActOnMenu;
        InputAction _selectLeftActOnMenu;
        InputAction _enterActOnMenu;
        InputAction _cancelActOnMenu;

        //プレイ中
        public InputAction MoveActOnPlayScene => _moveActOnPlayScene;
        public InputAction DownActOnPlayScene => _downActOnPlayScene;
        public InputAction RunActOnPlayScene => _runActOnPlayScene;
        public InputAction JumpActOnPlayScene => _jumpActOnPlayScene;
        public InputAction InteractActOnPlayScene => _interactActOnPlayScene;
        public InputAction ItemActOnPlayScene => _itemActOnPlayScene;
        public InputAction ItemSlotActOnPlayScene => _itemSlotActOnPlayScene;
        public InputAction SlotNextActOnPlayScene => _slotNextActOnPlayScene;
        public InputAction SlotBackActOnPlayScene => _slotBackActOnPlayScene;
        public InputAction MenuActOnPlayScene => _menuActOnPlayScene;
        //UI
        public InputAction MenuActOnUI => _menuActOnUI;
        public InputAction MenuSelectActOnUI => _menuSelectActOnUI;
        public InputAction ItemSlotActOnUI => _itemSlotActOnUI;
        public InputAction SlotNextActOnUI => _slotNextActOnUI;
        public InputAction SlotBackActOnUI => _slotBackActOnUI;
        public InputAction EnterActOnUI => _enterActOnUI;
        public InputAction CancelActOnUI => _cancelActOnUI;
        public InputAction SelectUpOnUI => _selectUpOnUI;
        public InputAction SelectDownOnUI => _selectDownOnUI;
        public InputAction SelectRightOnUI => _selectRightOnUI;
        public InputAction SelectLeftOnUI => _selectLeftOnUI;
        //アウトゲーム
        public InputAction EnterActOnOutGame => _enterActOnOutGame;
        public InputAction SelectUpOnOutGame => _selectUpOnOutGame;
        public InputAction SelectDownOnOutGame => _selectDownOnOutGame;
        //アウトゲームカテゴリー
        public InputAction MenuNextActOnOutGameCategory => _menuNextActOnOutGameCategory;
        public InputAction MenuBackActOnOutGameCategory => _menuBackActOnOutGameCategory;
        public InputAction MenuSelectActOnOutGameCategory => _menuSelectActOnOutGameCategory;
        public InputAction EnterActOnOutGameCategory => _enterActOnOutGameCategory;
        public InputAction CancelActOnOutGameCategory => _cancelActOnOutGameCategory;
        public InputAction SelectUpOnOutGameCategory => _selectUpOnOutGameCategory;
        public InputAction SelectDownOnOutGameCategory => _selectDownOnOutGameCategory;
        public InputAction SelectRightOnOutGameCategory => _selectRightOnOutGameCategory;
        public InputAction SelectLeftOnOutGameCategory => _selectLeftOnOutGameCategory;
        //メニュー
        public InputAction MenuSelectActOnMenu => _menuSelectActOnMenu;
        public InputAction SlotNextActOnMenu => _slotNextActOnMenu;
        public InputAction SlotBackActOnMenu => _slotBackActOnMenu;
        public InputAction SelectUpActOnMenu => _selectUpActOnMenu;
        public InputAction SelectDownActOnMenu => _selectDownActOnMenu;
        public InputAction SelectRightActOnMenu => _selectRightActOnMenu;
        public InputAction SelectLeftActOnMenu => _selectLeftActOnMenu;
        public InputAction EnterActOnMenu => _enterActOnMenu;
        public InputAction CancelActOnMenu => _cancelActOnMenu;
        #endregion

        private void Awake()
        {
            //Init();
        }

        public void Init()
        {
            if (!_instance)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                //プレイ中
                _player = _actions.FindActionMap(ActionMapName.Player.ToString());
                _moveActOnPlayScene = _player.FindAction("Move");
                _downActOnPlayScene = _player.FindAction("Down");
                _runActOnPlayScene = _player.FindAction("Run");
                _jumpActOnPlayScene = _player.FindAction("Jump");
                _interactActOnPlayScene = _player.FindAction("Interact");
                _itemActOnPlayScene = _player.FindAction("Item");
                _itemSlotActOnPlayScene = _player.FindAction("ItemSlot");
                _slotNextActOnPlayScene = _player.FindAction("SlotNext");
                _slotBackActOnPlayScene = _player.FindAction("SlotBack");
                _menuActOnPlayScene = _player.FindAction("Menu");

                //UI
                _ui = _actions.FindActionMap(ActionMapName.UI.ToString());
                _menuActOnUI = _ui.FindAction("Menu");
                _menuSelectActOnUI = _ui.FindAction("MenuSelect");
                _itemSlotActOnUI = _ui.FindAction("ItemSlot");
                _slotNextActOnUI = _ui.FindAction("SlotNext");
                _slotBackActOnUI = _ui.FindAction("SlotBack");
                _enterActOnUI = _ui.FindAction("Enter");
                _cancelActOnUI = _ui.FindAction("Cancel");
                _selectUpOnUI = _ui.FindAction("SelectUp");
                _selectDownOnUI = _ui.FindAction("SelectDown");
                _selectRightOnUI = _ui.FindAction("SelectRight");
                _selectLeftOnUI = _ui.FindAction("SelectLeft");

                //アウトゲーム
                _outGame = _actions.FindActionMap(ActionMapName.OutGame.ToString());
                _enterActOnOutGame = _outGame.FindAction("Enter");
                _selectUpOnOutGame = _outGame.FindAction("SelectUp");
                _selectDownOnOutGame = _outGame.FindAction("SelectDown");

                //アウトゲームカテゴリー
                _outGameCategory = _actions.FindActionMap(ActionMapName.OutGameCategory.ToString());
                _menuNextActOnOutGameCategory = _outGameCategory.FindAction("MenuNext");
                _menuBackActOnOutGameCategory = _outGameCategory.FindAction("MenuBack");
                _menuSelectActOnOutGameCategory = _outGameCategory.FindAction("MenuSelect");
                _enterActOnOutGameCategory = _outGameCategory.FindAction("Enter");
                _cancelActOnOutGameCategory = _outGameCategory.FindAction("Cancel");
                _selectUpOnOutGameCategory = _outGameCategory.FindAction("SelectUp");
                _selectDownOnOutGameCategory = _outGameCategory.FindAction("SelectDown");
                _selectRightOnOutGameCategory = _outGameCategory.FindAction("SelectRight");
                _selectLeftOnOutGameCategory = _outGameCategory.FindAction("SelectLeft");

                //メニュー
                _menu = _actions.FindActionMap(ActionMapName.Menu.ToString());
                _menuSelectActOnMenu = _menu.FindAction("MenuSelect");
                _slotNextActOnMenu = _menu.FindAction("SlotNext");
                _slotBackActOnMenu = _menu.FindAction("SlotBack");
                _selectUpActOnMenu = _menu.FindAction("SelectUp");
                _selectDownActOnMenu = _menu.FindAction("SelectDown");
                _selectRightActOnMenu = _menu.FindAction("SelectRight");
                _selectLeftActOnMenu = _menu.FindAction("SelectLeft");
                _enterActOnMenu = _menu.FindAction("Enter");
                _cancelActOnMenu = _menu.FindAction("Cancel");
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// アクションマップを切り替える関数
        /// </summary>
        /// <param name="mapName">切り替えるアクションマップの名前</param>
        public void ChangeActionMap(ActionMapName mapName)
        {
            switch (mapName)
            {
                case ActionMapName.Player:
                    _outGameCategory.Disable();
                    _outGame.Disable();
                    _ui.Disable();
                    _menu.Disable();
                    _player.Enable();
                    Debug.Log("CurrentMap is Player");
                    break;
                case ActionMapName.UI:
                    _outGameCategory.Disable();
                    _outGame.Disable();
                    _player.Disable();
                    _menu.Disable();
                    _ui.Enable();
                    Debug.Log("CurrentMap is UI");
                    break;
                case ActionMapName.OutGame:
                    _player.Disable();
                    _outGameCategory.Disable();
                    _ui.Disable();
                    _menu.Disable();
                    _outGame.Enable();
                    Debug.Log("CurrentMap is OutGame");
                    break;
                case ActionMapName.Menu:
                    _outGameCategory.Disable();
                    _outGame.Disable();
                    _ui.Disable();
                    _player.Disable();
                    _menu.Enable();
                    Debug.Log("CurrentMap is Menu");
                    break;
                case ActionMapName.OutGameCategory:
                    _player.Disable();
                    _outGame.Disable();
                    _ui.Disable();
                    _menu.Disable();
                    _outGameCategory.Enable();
                    Debug.Log("CurrentMap is OutGameCategory");
                    break;
                default:
                    Debug.LogError("No ActionMap Found");
                    break;
            }
        }

        /// <summary>
        /// 最後に入力したデバイスに応じた処理をする関数
        /// </summary>
        /// <param name="context"></param>
        void GetCurrentControlDevice(InputAction.CallbackContext context)
        {
            var device = context.control.device;
            if (_preDevice != device)
            {
                _preDevice = device;
                Debug.Log($"Device Changed : {device}");
                if (device is Gamepad)
                {
                }
                else if (device is Keyboard || device is UnityEngine.InputSystem.Mouse)
                {
                }
            }
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
    }
}
