using System;
using UnityEngine;

namespace DataDriven
{
    /// <summary>データの生成を司るベースクラス</summary>
    public abstract class SceneDataCreateFlow : ScriptableObject
    {
        protected RuntimeDataRepository _repository;

        /// <summary>
        /// データ生成を行う関数
        /// </summary>
        /// <param name="repository">ランタイムデータの保管庫</param>
        public abstract void CreateSceneData(RuntimeDataRepository repository);

        /// <summary>
        /// データ作成関数
        /// </summary>
        /// <typeparam name="TDefault">初期データの型</typeparam>
        /// <typeparam name="TRuntime">ランタイムデータの型</typeparam>
        /// <param name="id">ID</param>
        /// <param name="data">初期データ</param>
        /// <param name="factory">ランタイムデータ作成関数</param>
        protected void DataCreate<TDefault, TRuntime>(DataID id, TDefault data, Func<TDefault, TRuntime> factory) where TRuntime : IRuntime
        {
            if (_repository.TryGetData<TRuntime>(id, out var dummy)) return;

            //データ生成
            var runtime = factory(data);

            //保管庫に登録
            _repository.RegisterData<TRuntime>(id, runtime);
            Debug.Log($"Create => {typeof(TRuntime)}");
        }
    }
}


