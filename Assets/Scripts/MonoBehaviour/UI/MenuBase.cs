using Interface;
using UnityEngine;

/// <summary>メニューの項目のベースクラス</summary>
public class MenuBase : UIBehaviour
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
