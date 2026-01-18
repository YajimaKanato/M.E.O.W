using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    public class OutGameCategoryInput : InputBase
    {
        InputAction _menuNextActOnOutGameCategory;
        InputAction _menuBackActOnOutGameCategory;
        InputAction _menuSelectActOnOutGameCategory;
        InputAction _enterActOnOutGameCategory;
        InputAction _cancelActOnOutGameCategory;
        InputAction _selectUpOnOutGameCategory;
        InputAction _selectDownOnOutGameCategory;
        InputAction _selectRightOnOutGameCategory;
        InputAction _selectLeftOnOutGameCategory;

        public InputAction MenuNextActOnOutGameCategory => _menuNextActOnOutGameCategory;
        public InputAction MenuBackActOnOutGameCategory => _menuBackActOnOutGameCategory;
        public InputAction MenuSelectActOnOutGameCategory => _menuSelectActOnOutGameCategory;
        public InputAction EnterActOnOutGameCategory => _enterActOnOutGameCategory;
        public InputAction CancelActOnOutGameCategory => _cancelActOnOutGameCategory;
        public InputAction SelectUpOnOutGameCategory => _selectUpOnOutGameCategory;
        public InputAction SelectDownOnOutGameCategory => _selectDownOnOutGameCategory;
        public InputAction SelectRightOnOutGameCategory => _selectRightOnOutGameCategory;
        public InputAction SelectLeftOnOutGameCategory => _selectLeftOnOutGameCategory;

        public override void ActionMapSetting()
        {
            _actionMap = _actions.FindActionMap(_actionMapName.ToString());
            _menuNextActOnOutGameCategory = _actionMap.FindAction("MenuNext");
            _menuBackActOnOutGameCategory = _actionMap.FindAction("MenuBack");
            _menuSelectActOnOutGameCategory = _actionMap.FindAction("MenuSelect");
            _enterActOnOutGameCategory = _actionMap.FindAction("Enter");
            _cancelActOnOutGameCategory = _actionMap.FindAction("Cancel");
            _selectUpOnOutGameCategory = _actionMap.FindAction("SelectUp");
            _selectDownOnOutGameCategory = _actionMap.FindAction("SelectDown");
            _selectRightOnOutGameCategory = _actionMap.FindAction("SelectRight");
            _selectLeftOnOutGameCategory = _actionMap.FindAction("SelectLeft");
        }
    }
}
