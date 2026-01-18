using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    public class PlaySceneInput : InputBase
    {
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

        public override void ActionMapSetting()
        {
            _actionMap = _actions.FindActionMap(_actionMapName.ToString());
            _moveActOnPlayScene = _actionMap.FindAction("Move");
            _downActOnPlayScene = _actionMap.FindAction("Down");
            _runActOnPlayScene = _actionMap.FindAction("Run");
            _jumpActOnPlayScene = _actionMap.FindAction("Jump");
            _interactActOnPlayScene = _actionMap.FindAction("Interact");
            _itemActOnPlayScene = _actionMap.FindAction("Item");
            _itemSlotActOnPlayScene = _actionMap.FindAction("ItemSlot");
            _slotNextActOnPlayScene = _actionMap.FindAction("SlotNext");
            _slotBackActOnPlayScene = _actionMap.FindAction("SlotBack");
            _menuActOnPlayScene = _actionMap.FindAction("Menu");
        }
    }
}
