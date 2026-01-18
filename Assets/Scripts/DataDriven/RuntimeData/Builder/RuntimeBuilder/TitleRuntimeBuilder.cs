using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "TitleRuntimeBuilder", menuName = "RuntimeBuilder/TitleRuntimeBuilder")]
    public class TitleRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] TitleDefaultData _title;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<TitleRuntimeData>(id, out var dummy)) return;

            var data = new TitleRuntimeData(_title);
            repository.RegisterData(id, data);
        }
    }
}
