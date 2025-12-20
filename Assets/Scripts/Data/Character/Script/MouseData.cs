using Interface;
using UnityEngine;

/// <summary>ネズミのデータ</summary>
[CreateAssetMenu(fileName = "MouseData", menuName = "CharacterData/MouseData")]
public class MouseData : CharacterDataBase, ITalkable
{
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

/// <summary>ネズミのランタイムデータ</summary>
public class MouseRuntimeData : IRunTime
{
    public MouseRuntimeData(MouseData data) { }
}
