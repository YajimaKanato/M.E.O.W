using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "LogRuntimeBuilder", menuName = "RuntimeBuilder/LogRuntimeBuilder")]
    public class LogRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] LogDefaultData _log;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<LogRuntimeData>(id, out var dummy)) return;

            var data = new LogRuntimeData(_log);
            repository.RegisterData(id, data);
        }
    }
}
