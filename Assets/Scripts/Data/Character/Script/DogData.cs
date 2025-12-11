using Interface;
using UnityEngine;

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

public class DogRnutimeData : IRunTime
{
    public DogRnutimeData(DogData data)
    {

    }
}
