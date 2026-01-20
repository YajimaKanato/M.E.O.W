using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "MouseRuntimeBuilder", menuName = "RuntimeBuilder/MouseRuntimeBuilder")]
    public class MouseRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] MouseDefaultData _mouse;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<MouseRuntimeData>(id, out var dummy)) return false;

            var data = new MouseRuntimeData(_mouse);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
