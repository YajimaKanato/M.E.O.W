using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーの入力受付を司るクラス</summary>
    public class PlayerInput : MonoBehaviour
    {
        GameFlowManager _gameFlowManager;
        CharacterRuntime _character;

        #region 直接Viewに関わらない入力
        /// <summary>
        /// インタラクトを行う関数
        /// Tキー/Aボタンに対応
        /// </summary>
        public void Interact()
        {
            _gameFlowManager.Interact(_character);
        }

        /// <summary>
        /// 意思決定をする関数
        /// Eキー、エンターキー/Aボタンに対応
        /// </summary>
        public void Confirm()
        {
            _gameFlowManager.Confirm();
        }

        /// <summary>
        /// キャンセルをする関数
        /// Cキー/Bボタンに対応
        /// </summary>
        public void Cancel()
        {

        }

        /// <summary>
        /// アイテムを使用する関数
        /// Eキー/LTボタンに対応
        /// </summary>
        public void UseItem()
        {

        }

        /// <summary>
        /// ホットバーのスロットを選択する関数
        /// 1～6キー/LRボタンに対応
        /// </summary>
        public void HotbarSelect()
        {

        }

        /// <summary>
        /// アイテムリストの要素を選択する関数
        /// 十字キー、WASDキー/左スティック、DPadに対応
        /// </summary>
        public void ItemListSelect()
        {

        }
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = collision.gameObject;
            if (go.CompareTag("Character"))
            {
                if (go.TryGetComponent(out CharacterRuntime character))
                {
                    _character = character;
                }
            }
        }
    }
}
