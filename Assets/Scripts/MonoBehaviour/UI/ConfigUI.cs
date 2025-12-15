using Interface;
using UnityEngine;

/// <summary>設定に関する制御を行うクラス</summary>
public class ConfigUI : MenuBase
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
