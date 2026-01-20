using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "InfoRuntimeBuilder", menuName = "RuntimeBuilder/InfoRuntimeBuilder")]
    public class InfoRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] InfoDefaultData _info;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<InfoRuntimeData>(id, out var dummy)) return false;

            var data = new InfoRuntimeData(_info);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
