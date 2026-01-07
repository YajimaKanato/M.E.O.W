using UnityEngine;

namespace DataDriven
{
    /// <summary>設定のランタイムデータ</summary>
    public class ConfigRuntimeData
    {
        ConfigDefaultData _config;

        public ConfigRuntimeData(ConfigDefaultData config)
        {
            _config = config;
        }
    }
}
