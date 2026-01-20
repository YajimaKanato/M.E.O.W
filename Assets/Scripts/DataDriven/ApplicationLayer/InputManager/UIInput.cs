using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    public class UIInput : InputBase
    {
        InputAction _menuActOnUI;
        InputAction _itemSlotActOnUI;
        InputAction _slotNextActOnUI;
        InputAction _slotBackActOnUI;
        InputAction _enterActOnUI;
        InputAction _cancelActOnUI;
        InputAction _selectRightOnUI;
        InputAction _selectLeftOnUI;

        public InputAction MenuActOnUI => _menuActOnUI;
        public InputAction ItemSlotActOnUI => _itemSlotActOnUI;
        public InputAction SlotNextActOnUI => _slotNextActOnUI;
        public InputAction SlotBackActOnUI => _slotBackActOnUI;
        public InputAction EnterActOnUI => _enterActOnUI;
        public InputAction CancelActOnUI => _cancelActOnUI;
        public InputAction SelectRightOnUI => _selectRightOnUI;
        public InputAction SelectLeftOnUI => _selectLeftOnUI;

        public override void ActionMapSetting()
        {
            _actionMap = _actions.FindActionMap(_actionMapName.ToString());
            _menuActOnUI = _actionMap.FindAction("Menu");
            _itemSlotActOnUI = _actionMap.FindAction("ItemSlot");
            _slotNextActOnUI = _actionMap.FindAction("SlotNext");
            _slotBackActOnUI = _actionMap.FindAction("SlotBack");
            _enterActOnUI = _actionMap.FindAction("Enter");
            _cancelActOnUI = _actionMap.FindAction("Cancel");
            _selectRightOnUI = _actionMap.FindAction("SelectRight");
            _selectLeftOnUI = _actionMap.FindAction("SelectLeft");
        }
    }
}
