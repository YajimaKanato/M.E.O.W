using UnityEngine;

namespace DataDriven
{
    /// <summary>会話ログのランタイムデータ</summary>
    public class LogRuntimeData : MenuCategoryRuntime
    {
        LogDefaultData _log;

        public LogRuntimeData(LogDefaultData log)
        {
            _log = log;
        }

        public override void SelectCategory(IndexMove move)
        {

        }
    }
}
