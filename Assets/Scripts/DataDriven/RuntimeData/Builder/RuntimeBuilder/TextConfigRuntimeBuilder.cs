using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "TextConfigRuntimeBuilder", menuName = "RuntimeBuilder/TextConfigRuntimeBuilder")]
    public class TextConfigRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] TextConfigDefaultData _textConfig;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<TextConfigRuntimeData>(id, out var dummy)) return false;

            var data = new TextConfigRuntimeData(_textConfig);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
