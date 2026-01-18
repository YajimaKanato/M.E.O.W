using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "AndroidRuntimeBuilder", menuName = "RuntimeBuilder/AndroidRuntimeBuilder")]
    public class AndroidRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] AndroidDefaultData _android;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<AndroidRuntimeData>(id, out var dummy)) return;

            var data = new AndroidRuntimeData(_android);
            repository.RegisterData(id, data);
        }
    }
}
