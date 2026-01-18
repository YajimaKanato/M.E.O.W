using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>会話ログのランタイムデータ</summary>
    public class LogRuntimeData : IVerticalArrowInput, IRuntime
    {
        LogDefaultData _log;
        List<string> _logList;
        int _currentIndex;
        int _maxLogCount;

        public List<string> LogList => _logList;
        public int CurrentIndex => _currentIndex;

        public LogRuntimeData(LogDefaultData log)
        {
            _log = log;
            _logList = new List<string>();
            _maxLogCount = log.MaxLogCount;
        }

        /// <summary>
        /// 会話ログを記憶する関数
        /// </summary>
        /// <param name="log">記憶する会話ログ</param>
        public void MemorizeLog(string log)
        {
            if (_logList.Count >= _maxLogCount) _logList.RemoveAt(0);
            _logList.Add(log);
        }

        /// <summary>
        /// 選択中の会話ログを返す関数
        /// </summary>
        /// <returns>選択中の会話ログ</returns>
        public string GetLog()
        {
            if (_currentIndex < 0 || _logList.Count - 1 < _currentIndex) return null;
            return _logList[_currentIndex];
        }

        public void SelectCategory(IndexMove move)
        {
            _currentIndex += (int)move;
            if (_currentIndex < 0) _currentIndex = 0;
            if (_currentIndex > _maxLogCount - 1) _currentIndex = _maxLogCount - 1;
        }
    }
}
