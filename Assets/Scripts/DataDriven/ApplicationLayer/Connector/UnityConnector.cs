using System;
using UnityEngine;

namespace DataDriven
{
    /// <summary>Unityとデータをつなげる役割のクラス</summary>
    public class UnityConnector
    {
        PlayerActionConnector _actionConnector;
        MenuConnector _menuConnector;

        public PlayerActionConnector ActionConnector => _actionConnector;
        public MenuConnector MenuConnector => _menuConnector;

        public void Init()
        {
            _actionConnector = new PlayerActionConnector();
            _menuConnector = new MenuConnector();
        }
    }
}
