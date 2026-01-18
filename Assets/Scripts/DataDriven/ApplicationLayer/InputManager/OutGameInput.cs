using UnityEngine;
using UnityEngine.InputSystem;

namespace DataDriven
{
    /// <summary>アウトゲームの入力管理をするクラス</summary>
    public class OutGameInput : InputBase
    {
        InputAction _enterActOnOutGame;
        InputAction _selectUpOnOutGame;
        InputAction _selectDownOnOutGame;

        public InputAction EnterActOnOutGame => _enterActOnOutGame;
        public InputAction SelectUpOnOutGame => _selectUpOnOutGame;
        public InputAction SelectDownOnOutGame => _selectDownOnOutGame;

        public override void ActionMapSetting()
        {
            _actionMap = _actions.FindActionMap(_actionMapName.ToString());
            _enterActOnOutGame = _actionMap.FindAction("Enter");
            _selectUpOnOutGame = _actionMap.FindAction("SelectUp");
            _selectDownOnOutGame = _actionMap.FindAction("SelectDown");
        }
    }
}
