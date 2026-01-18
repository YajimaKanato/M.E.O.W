using System;
using UnityEngine;

namespace DataDriven
{
    /// <summary>データの生成を司るベースクラス</summary>
    [CreateAssetMenu(fileName = "SceneDataCreateFlow", menuName = "DataCreateFlow/SceneDataCreateFlow")]
    public class SceneDataCreateFlow : ScriptableObject
    {
        [SerializeField] RuntimeDataBuilders[] _runtimeDataBuilders;
        RuntimeDataRepository _repository;

        /// <summary>
        /// データ生成を行う関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        public void CreateSceneData(RuntimeDataRepository repository)
        {
            _repository = repository;

            foreach (var data in _runtimeDataBuilders)
            {
                DataCreate(data.ID, data.Builder);
            }
        }

        /// <summary>
        /// データ作成関数
        /// </summary>
        /// <typeparam name="TDefault">初期データの型</typeparam>
        /// <param name="id">ID</param>
        /// <param name="data">初期データ</param>
        protected void DataCreate<TDefault>(DataID id, TDefault data) where TDefault : RuntimeBuilderBase
        {
            //データ生成
            data.CreateRuntime(_repository, id);
        }
    }

    [System.Serializable]
    public class RuntimeDataBuilders
    {
        [SerializeField] RuntimeBuilderBase _builder;
        [SerializeField] DataID _id;

        public RuntimeBuilderBase Builder => _builder;
        public DataID ID => _id;
    }
}


