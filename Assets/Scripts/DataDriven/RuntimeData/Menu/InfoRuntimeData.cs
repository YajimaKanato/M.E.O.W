using UnityEngine;

namespace DataDriven
{
    /// <summary>操作説明のランタイムデータ</summary>
    public class InfoRuntimeData : MenuCategoryRuntime, IRuntime
    {
        InfoDefaultData _info;

        public InfoRuntimeData(InfoDefaultData info)
        {
            _info = info;
        }

        public override void SelectCategory(SlotMoveVertical move)
        {

        }
    }
}
