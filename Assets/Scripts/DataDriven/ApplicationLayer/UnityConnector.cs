using System;
using UnityEngine;

namespace DataDriven
{
    /// <summary>Unityとデータをつなげる役割のクラス</summary>
    public class UnityConnector
    {
        PlayerActionConnector _actionConnector;

        public PlayerActionConnector ActionConnector => _actionConnector;

        public void Init()
        {
            _actionConnector = new PlayerActionConnector();
        }
    }
}
