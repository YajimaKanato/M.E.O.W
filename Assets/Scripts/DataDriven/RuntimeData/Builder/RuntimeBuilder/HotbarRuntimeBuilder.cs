using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "HotbarRuntimeBuilder", menuName = "RuntimeBuilder/HotbarRuntimeBuilder")]
    public class HotbarRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] HotbarDefaultData _hotbar;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<HotbarRuntimeData>(id, out var dummy)) return;

            var data = new HotbarRuntimeData(_hotbar);
            repository.RegisterData(id, data);
        }
    }
}
