using UnityEngine;

namespace DataDriven
{
    /// <summary>犬のランタイムデータの作成を司るクラス</summary>
    public class DogRuntimeDataFactory
    {
        /// <summary>
        /// 犬のランタイムデータを作成する関数
        /// </summary>
        /// <param name="dog">犬の初期データ</param>
        /// <returns>犬のランタイムデータ</returns>
        public DogRuntime DogCreate(DogDefaultData dog)
        {
            return new DogRuntime(dog);
        }
    }
}
