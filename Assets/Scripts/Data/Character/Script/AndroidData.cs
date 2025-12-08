using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "AndroidData", menuName = "CharacterData/AndroidData")]
public class AndroidData : CharacterDataBase, ITalkable
{
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

public class AndroidRuntimeData : IRunTime
{
    public AndroidRuntimeData(AndroidData data)
    {

    }
}
