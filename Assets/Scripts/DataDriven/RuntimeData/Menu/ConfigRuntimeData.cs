using UnityEngine;

namespace DataDriven
{
    /// <summary>設定のランタイムデータ</summary>
    public class ConfigRuntimeData : IVerticalArrowInput, IRuntime
    {
        ConfigDefaultData _config;
        ConfigType[] _categories;
        int _currentIndex;

        public ConfigType Category => _categories[_currentIndex];

        public ConfigRuntimeData(ConfigDefaultData config)
        {
            _config = config;
            _categories = config.Categories;
            _currentIndex = (int)config.DefaultCategory;
        }

        public void SelectCategory(IndexMove move)
        {
            _currentIndex += (int)move;
            if (_currentIndex < 0) _currentIndex = 0;
            if (_currentIndex > _categories.Length - 1) _currentIndex = _categories.Length - 1;
        }
    }
}
