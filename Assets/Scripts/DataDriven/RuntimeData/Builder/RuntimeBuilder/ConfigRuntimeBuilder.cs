using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "ConfigRuntimeBuilder", menuName = "RuntimeBuilder/ConfigRuntimeBuilder")]
    public class ConfigRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] ConfigDefaultData _config;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<ConfigRuntimeData>(id, out var dummy)) return false;

            var data = new ConfigRuntimeData(_config);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
