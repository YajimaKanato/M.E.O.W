using UnityEngine;

namespace DataDriven
{
    /// <summary>会話ログのランタイムデータ</summary>
    public class LogRuntimeData
    {
        LogDefaultData _log;

        public LogRuntimeData(LogDefaultData log)
        {
            _log = log;
        }
    }
}
