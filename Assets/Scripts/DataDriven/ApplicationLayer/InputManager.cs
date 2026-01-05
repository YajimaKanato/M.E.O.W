using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>入力受付を司るクラス</summary>
    public class InputManager : MonoBehaviour
    {
        [SerializeField, Tooltip("アクションアセット")] InputActionAsset _actions;
        [SerializeField, Tooltip("シーン開始時のアクションマップ")] ActionMapName _startActionMapName = ActionMapName.Unknown;
        InputDevice _preDevice;
        InputActionMap _player, _ui, _outGame;
        Stack<ActionMapName> _actionMapStack;
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
        InputAction _menuNextActOnOutGame;
        InputAction _menuBackActOnOutGame;
        InputAction _menuSelectActOnOutGame;
        InputAction _enterActOnOutGame;
        InputAction _cancelActOnOutGame;
        InputAction _selectUpOnOutGame;
        InputAction _selectDownOnOutGame;
        InputAction _selectRightOnOutGame;
        InputAction _selectLeftOnOutGame;

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
        public InputAction MenuNextActOnOutGame => _menuNextActOnOutGame;
        public InputAction MenuBackActOnOutGame => _menuBackActOnOutGame;
        public InputAction MenuSelectActOnOutGame => _menuSelectActOnOutGame;
        public InputAction EnterActOnOutGame => _enterActOnOutGame;
        public InputAction CancelActOnOutGame => _cancelActOnOutGame;
        public InputAction SelectUpOnOutGame => _selectUpOnOutGame;
        public InputAction SelectDownOnOutGame => _selectDownOnOutGame;
        public InputAction SelectRightOnOutGame => _selectRightOnOutGame;
        public InputAction SelectLeftOnOutGame => _selectLeftOnOutGame;
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
                _menuNextActOnOutGame = _outGame.FindAction("MenuNext");
                _menuBackActOnOutGame = _outGame.FindAction("MenuBack");
                _menuSelectActOnOutGame = _outGame.FindAction("MenuSelect");
                _enterActOnOutGame = _outGame.FindAction("Enter");
                _cancelActOnOutGame = _outGame.FindAction("Cancel");
                _selectUpOnOutGame = _outGame.FindAction("SelectUp");
                _selectDownOnOutGame = _outGame.FindAction("SelectDown");
                _selectRightOnOutGame = _outGame.FindAction("SelectRight");
                _selectLeftOnOutGame = _outGame.FindAction("SelectLeft");
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
        public void ChangeActionMap(ActionMapName mapName = ActionMapName.Unknown)
        {
            //引数に何も指定しなかったらスタックからポップ
            if (mapName == ActionMapName.Unknown)
            {
                _actionMapStack.Pop();
            }
            else
            {
                _actionMapStack.Push(mapName);
            }

            switch (_actionMapStack.Peek())
            {
                case ActionMapName.Player:
                    _outGame.Disable();
                    _ui.Disable();
                    _player.Enable();
                    Debug.Log("CurrentMap is Player");
                    break;
                case ActionMapName.UI:
                    _outGame.Disable();
                    _player.Disable();
                    _ui.Enable();
                    Debug.Log("CurrentMap is UI");
                    break;
                case ActionMapName.OutGame:
                    _player.Disable();
                    _ui.Disable();
                    _outGame.Enable();
                    Debug.Log("CurrentMap is OutGame");
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
        public void RegisterAct(InputAction act, Action<InputAction.CallbackContext> context)
        {
            act.started += context;
        }
    }
}
