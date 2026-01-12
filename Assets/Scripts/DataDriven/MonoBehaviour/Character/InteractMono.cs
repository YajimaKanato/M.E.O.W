using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクト可能なシーン上のオブジェクトのクラス</summary>
    public class InteractMono : SceneEntity
    {
        GameFlowManager _gameFlowManager;
        public override void Init()
        {
            tag = TagName.CHARACTER;
            _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }
    }
}
