using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクト可能なシーン上のオブジェクトのクラス</summary>
    public class InteractMono : SceneEntity
    {
        PlaySceneFlow _playSceneFlow;

        public override void Init()
        {
            tag = TagName.CHARACTER;
            _playSceneFlow = FindFirstObjectByType<PlaySceneFlow>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagName.PLAYER))
                _playSceneFlow.AddTargetList(this);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(TagName.PLAYER))
                _playSceneFlow.RemoveTargetList(this);
        }
    }
}
