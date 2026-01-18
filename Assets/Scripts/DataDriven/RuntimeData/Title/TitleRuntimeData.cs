using UnityEngine;

namespace DataDriven
{
    /// <summary>タイトルのランタイムデータ</summary>
    public class TitleRuntimeData : IRuntime
    {
        TitleDefaultData _title;
        int _currentType;
        TitleCategory[] _titleCategories;

        public int CurrentType => _currentType;

        public TitleRuntimeData(TitleDefaultData title)
        {
            _title = title;
            _currentType = (int)title.DefaultSelectIndex;
            _titleCategories = _title.Categories;
        }

        /// <summary>
        /// 現在選択中のタイトル項目を返す関数
        /// </summary>
        /// <returns>現在選択中のタイトル項目</returns>
        public TitleCategory GetTitleCategory()
        {
            return _titleCategories[_currentType];
        }

        /// <summary>
        /// タイトル項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void ChangeType(IndexMove dir)
        {
            _currentType += (int)dir;
            if (_currentType > _titleCategories.Length - 1)
            {
                _currentType = _titleCategories.Length - 1;
            }
            else if (_currentType < 0)
            {
                _currentType = 0;
            }
        }
    }
}
