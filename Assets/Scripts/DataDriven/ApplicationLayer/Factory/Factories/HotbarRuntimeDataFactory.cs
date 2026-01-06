using UnityEngine;

namespace DataDriven
{
    /// <summary>ホットバーのランタイムデータの作成を司るクラス</summary>
    public class HotbarRuntimeDataFactory
    {
        /// <summary>
        /// ホットバーのランタイムデータを作成する関数
        /// </summary>
        /// <param name="hotbar">ホットバーの初期データ</param>
        /// <returns>ホットバーのランタイムデータ</returns>
        public HotbarRuntimeData HotbarCreate(HotbarDefaultData hotbar)
        {
            return new HotbarRuntimeData(hotbar);
        }
    }
}
