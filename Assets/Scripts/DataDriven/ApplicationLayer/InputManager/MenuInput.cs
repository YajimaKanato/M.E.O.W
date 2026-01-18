using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    public class MenuInput : InputBase
    {
        InputAction _menuSelectActOnMenu;
        InputAction _slotNextActOnMenu;
        InputAction _slotBackActOnMenu;
        InputAction _selectUpActOnMenu;
        InputAction _selectDownActOnMenu;
        InputAction _selectRightActOnMenu;
        InputAction _selectLeftActOnMenu;
        InputAction _enterActOnMenu;
        InputAction _cancelActOnMenu;

        public InputAction MenuSelectActOnMenu => _menuSelectActOnMenu;
        public InputAction SlotNextActOnMenu => _slotNextActOnMenu;
        public InputAction SlotBackActOnMenu => _slotBackActOnMenu;
        public InputAction SelectUpActOnMenu => _selectUpActOnMenu;
        public InputAction SelectDownActOnMenu => _selectDownActOnMenu;
        public InputAction SelectRightActOnMenu => _selectRightActOnMenu;
        public InputAction SelectLeftActOnMenu => _selectLeftActOnMenu;
        public InputAction EnterActOnMenu => _enterActOnMenu;
        public InputAction CancelActOnMenu => _cancelActOnMenu;

        public override void ActionMapSetting()
        {
            _actionMap = _actions.FindActionMap(_actionMapName.ToString());
            _menuSelectActOnMenu = _actionMap.FindAction("MenuSelect");
            _slotNextActOnMenu = _actionMap.FindAction("SlotNext");
            _slotBackActOnMenu = _actionMap.FindAction("SlotBack");
            _selectUpActOnMenu = _actionMap.FindAction("SelectUp");
            _selectDownActOnMenu = _actionMap.FindAction("SelectDown");
            _selectRightActOnMenu = _actionMap.FindAction("SelectRight");
            _selectLeftActOnMenu = _actionMap.FindAction("SelectLeft");
            _enterActOnMenu = _actionMap.FindAction("Enter");
            _cancelActOnMenu = _actionMap.FindAction("Cancel");
        }
    }
}
