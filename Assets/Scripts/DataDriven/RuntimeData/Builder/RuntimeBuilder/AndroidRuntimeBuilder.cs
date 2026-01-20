using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "AndroidRuntimeBuilder", menuName = "RuntimeBuilder/AndroidRuntimeBuilder")]
    public class AndroidRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] AndroidDefaultData _android;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<AndroidRuntimeData>(id, out var dummy)) return false;

            var data = new AndroidRuntimeData(_android);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
