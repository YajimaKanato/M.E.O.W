using UnityEngine;

namespace DataDriven
{
    public abstract class ConfigCategory : ScriptableObject
    {
        [SerializeField] ConfigType _configType;
    }
}
