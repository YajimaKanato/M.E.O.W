using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクト可能なシーン上のオブジェクトのクラス</summary>
    public class InteractMono : SceneEntity
    {
        GameFlowManager _gameFlowManager;
        public override void Init(UnityConnector connector)
        {
            tag = TagName.CHARACTER;
            _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagName.PLAYER))
                _gameFlowManager.AddTargetList(this);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagName.PLAYER))
                _gameFlowManager.RemoveTargetList(this);
        }
    }
}
