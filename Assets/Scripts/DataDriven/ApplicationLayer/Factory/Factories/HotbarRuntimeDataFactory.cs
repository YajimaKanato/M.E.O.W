using UnityEngine;

namespace DataDriven
{
    /// <summary>ホットバーのランタイムデータの作成を司るクラス</summary>
    public class HotbarRuntimeDataFactory
    {
        /// <summary>
        /// ホットバーのランタイムデータを作成する関数
        /// </summary>
        /// <returns>ホットバーのランタイムデータ</returns>
        public HotbarRuntimeData HotbarCreate()
        {
            return new HotbarRuntimeData();
        }
    }
}
