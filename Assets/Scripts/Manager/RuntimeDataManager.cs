using Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゲームにおけるデータを管理するクラス</summary>
public class RuntimeDataManager : ManagerBase
{
    Dictionary<Type, object> _dataStores;
    static RuntimeDataManager _instance;

    public override bool Init(GameManager manager)
    {
        if (_instance == null) _isInitialized = InitializeManager.InitializationForVariable(out _dataStores, new Dictionary<Type, object>());
        return _isInitialized;
    }

    /// <summary>
    /// データ保管クラスにデータを登録する関数
    /// </summary>
    /// <typeparam name="T">データの型</typeparam>
    /// <param name="id">ID</param>
    /// <param name="data">データ</param>
    public void RegisterData<T>(int id, T data) where T : IRunTime
    {
        //データ型を受け取る
        var type = typeof(T);
        //データ型に対応するデータ保管クラスがなければ作成
        if (!_dataStores.ContainsKey(type)) _dataStores[type] = new RunTimeDataStore<T>();
        //IDに対応したデータが登録されていなければ
        //データ保管クラスが保持するデータの保管場所にIDとデータをセットにして登録
        if (!((RunTimeDataStore<T>)_dataStores[type]).GetData(id, out var dummy)) ((RunTimeDataStore<T>)_dataStores[type]).DataRegister(id, data);
    }

    /// <summary>
    /// IDに対応した任意の型のデータを取得する関数
    /// </summary>
    /// <typeparam name="T">取得したいデータの型</typeparam>
    /// <param name="id">ID</param>
    /// <returns>データ</returns>
    public T GetData<T>(int id) where T : IRunTime
    {
        //データ型を受け取る
        var type = typeof(T);
        //データ型が登録されてなかったらreturn
        if (!_dataStores.ContainsKey(type)) return default;
        //データを保管しているクラスを取得
        var store = (RunTimeDataStore<T>)_dataStores[type];
        //データを保管するクラスがなければreturn
        if (store == null) return default;
        //データを取得
        store.GetData(id, out var data);
        return data;
    }

    /// <summary>
    /// データをリセットする関数
    /// </summary>
    public void DataReset()
    {
        _instance = null;
        Debug.Log($"DataManager has Cleaned");
    }
}

/// <summary>任意の型のデータをIDと対応させて制御するクラス</summary>
/// <typeparam name="T">データの型</typeparam>
public class RunTimeDataStore<T> where T : IRunTime
{
    /// <summary>データとIDをセットにして保持する辞書</summary>
    Dictionary<int, T> _dataStore = new();

    /// <summary>
    /// データをIDとセットにして辞書に登録する関数
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="data">データ</param>
    public void DataRegister(int id, T data)
    {
        _dataStore[id] = data;
    }

    /// <summary>
    /// IDに対応したデータを取得する関数
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="data">IDに対応したデータ</param>
    /// <returns>データを取得できたかどうか</returns>
    public bool GetData(int id, out T data)
    {
        return _dataStore.TryGetValue(id, out data);
    }
}
