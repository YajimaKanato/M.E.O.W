using UnityEngine;

[CreateAssetMenu(fileName = "TitleData", menuName = "UIData/TitleData")]
public class TitleData : UIDataBase
{
    [SerializeField] int _titleCategoryCount = 5;
    public int TitleCategoryCount => _titleCategoryCount;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

namespace Title
{
    public enum TitleCategory
    {
        Start,
        EndingList,
        Option,
        Credit,
        Reset

    }
}
