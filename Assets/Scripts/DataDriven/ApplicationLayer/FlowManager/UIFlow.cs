using UnityEngine;

namespace DataDriven
{
    public class UIFlow : FlowBase
    {
        InteractSystem _interactSystem;

        public override void Init(GameFlowManager gameFlowManager, RuntimeDataRepository repository, UnityConnector connector)
        {
            _gameFlowManager = gameFlowManager;
            _interactSystem = new InteractSystem(gameFlowManager, repository);
        }

        /// <summary>
        /// インタラクトが行われたときに呼ばれる関数
        /// </summary>
        /// <param name="character">インタラクトを行う対象のキャラクター</param>
        public void Interact(DataID character)
        {
            if (_interactSystem.StartInteract(character)) _gameFlowManager.ChangeActionMap(ActionMapName.UI);
        }

        /// <summary>
        /// インタラクトを進める時に呼ばれる関数
        /// </summary>
        public void Confirm()
        {
            if (_interactSystem.PushInteract()) _gameFlowManager.ChangeActionMap();
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectOnConversationForKeyboard(int index)
        {
            _interactSystem.HotbarSelectForKetboard(index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarselectOnConversationForGamePad(IndexMove dir)
        {
            _interactSystem.HotbarSelectForGamePad(dir);
        }

        /// <summary>
        /// 意思決定をする関数
        /// </summary>
        /// <param name="decide"></param>
        public void Decide(bool decide)
        {
            _interactSystem.Decide(decide);
        }
    }
}
