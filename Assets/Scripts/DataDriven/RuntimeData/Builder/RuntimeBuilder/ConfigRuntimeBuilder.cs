using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "ConfigRuntimeBuilder", menuName = "RuntimeBuilder/ConfigRuntimeBuilder")]
    public class ConfigRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] ConfigDefaultData _config;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<ConfigRuntimeData>(id, out var dummy)) return;

            var data = new ConfigRuntimeData(_config);
            repository.RegisterData(id, data);
        }
    }
}
