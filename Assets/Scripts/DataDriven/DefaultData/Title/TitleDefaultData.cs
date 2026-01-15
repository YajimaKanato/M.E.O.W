using UnityEngine;

namespace DataDriven
{
    /// <summary>タイトルの初期データ</summary>
    [CreateAssetMenu(fileName = "TitleDefaultData", menuName = "Title/TitleDefaultData")]
    public class TitleDefaultData : ScriptableObject
    {
        [SerializeField] TitleCategory[] _categories;
        [SerializeField] TitleCategory _defaultIndex;

        public TitleCategory[] Categories => _categories;
        public TitleCategory DefaultSelectIndex => _defaultIndex;
    }
}
