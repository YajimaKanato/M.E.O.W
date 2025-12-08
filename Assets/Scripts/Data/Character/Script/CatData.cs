using Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "CatData", menuName = "CharacterData/CatData")]
public class CatData : CharacterDataBase, ITalkable
{
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

public class CatRuntimeData : IRunTime
{
    public CatRuntimeData(CatData data)
    {

    }
}
