using Interface;
using UnityEngine;

/// <summary>メニューの項目のベースクラス</summary>
public abstract class MenuBase : UIBehaviour
{
    /// <summary>
    /// メニューを開くときに各項目の初期化を行う関数
    /// </summary>
    public abstract void OpenSetting();
}


