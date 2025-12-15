using Interface;
using UnityEngine;

public class ConfigUI : MenuSelect
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
