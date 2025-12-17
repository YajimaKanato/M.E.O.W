using Interface;
using UnityEngine;

/// <summary>犬のデータ</summary>
[CreateAssetMenu(fileName = "DogData", menuName = "CharacterData/DogData")]
public class DogData : CharacterDataBase, ITalkable
{
    public string CharacterName => _characterName;
    public Sprite CharacterImage => _characterImage;
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

/// <summary>犬のランタイムデータ</summary>
public class DogRnutimeData : IRunTime
{
    public DogRnutimeData(DogData data) { }
}
