using UnityEngine;

namespace DataDriven
{
    /// <summary>ゲームフローのベースクラス</summary>
    public abstract class FlowBase : MonoBehaviour
    {
        protected GameFlowManager _gameFlowManager;

        public abstract void Init(GameFlowManager gameFlowManager, RuntimeDataRepository repository, UnityConnector connector);
    }
}
