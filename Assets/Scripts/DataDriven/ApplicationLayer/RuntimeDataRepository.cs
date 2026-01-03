using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>すべてのランタイムデータを保持するクラス</summary>
    public class RuntimeDataRepository
    {
        Dictionary<Type, object> _dataStores;

        /// <summary>
        /// 保管庫を取得する関数
        /// </summary>
        /// <typeparam name="T">取得する保管庫が保持するデータ型</typeparam>
        /// <returns>保管庫</returns>
        RuntimeDataStore<T> GetOrCreateStore<T>() where T : IRuntime
        {
            //データ型を受け取る
            var type = typeof(T);
            //データ型に対応する保管庫がなければ作成
            if (!_dataStores.TryGetValue(type, out var store))
            {
                store = new RuntimeDataStore<T>();
                _dataStores[type] = store;
            }
            //保管庫の情報を返す
            return (RuntimeDataStore<T>)store;
        }

        /// <summary>
        /// 保管庫を取得する関数
        /// </summary>
        /// <typeparam name="T">取得する保管庫が保持するデータ型</typeparam>
        /// <returns>保管庫</returns>
        RuntimeDataStore<T> GetStore<T>() where T : IRuntime
        {
            //保管庫の情報を返す
            return _dataStores.TryGetValue(typeof(T), out var store) ? (RuntimeDataStore<T>)store : null;
        }

        /// <summary>
        /// データ保管クラスにデータを登録する関数
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="id">ID</param>
        /// <param name="data">データ</param>
        public void RegisterData<T>(int id, T data) where T : IRuntime
        {
            //保管庫を取得
            var store = GetOrCreateStore<T>();
            //IDに対応したデータが登録されていなければ
            //データ保管クラスが保持するデータの保管場所にIDとデータをセットにして登録
            if (store.GetData(id) == null) store.RegisterData(id, data);
        }

        /// <summary>
        /// IDに対応した任意の型のデータを取得する関数
        /// </summary>
        /// <typeparam name="T">取得したいデータの型</typeparam>
        /// <param name="id">ID</param>
        /// <returns>データ</returns>
        public bool TryGetData<T>(int id, out T data) where T : IRuntime
        {
            //保管庫を取得
            var store = GetStore<T>();
            //取得できなければそのことを返す
            if (store == null)
            {
                data = default;
                return false;
            }
            //データを取得
            data = store.GetData(id);
            //データを取得できたかどうかを返す
            return store != null;
        }

        /// <summary>
        /// IDに対応したデータを削除する関数
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="id">削除するデータに対応するID</param>
        public void RemoveData<T>(int id) where T : IRuntime
        {
            //データを削除
            GetStore<T>()?.RemoveData(id);
        }
    }

    /// <summary>任意の型のデータをIDと対応させて制御するクラス</summary>
    /// <typeparam name="T">データの型</typeparam>
    public class RuntimeDataStore<T> where T : IRuntime
    {
        /// <summary>データとIDをセットにして保持する辞書</summary>
        Dictionary<int, T> _dataStore = new();

        /// <summary>
        /// データをIDとセットにして辞書に登録する関数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="data">データ</param>
        public void RegisterData(int id, T data)
        {
            _dataStore[id] = data;
        }

        /// <summary>
        /// IDに対応したデータを取得する関数
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>取得したデータ</returns>
        public T GetData(int id)
        {
            return _dataStore.TryGetValue(id, out var data) ? data : default;
        }

        /// <summary>
        /// IDに対応したデータを削除する関数
        /// </summary>
        /// <param name="id">削除するデータに対応するID</param>
        public void RemoveData(int id)
        {
            if (_dataStore.ContainsKey(id)) _dataStore.Remove(id);
        }
    }
}
