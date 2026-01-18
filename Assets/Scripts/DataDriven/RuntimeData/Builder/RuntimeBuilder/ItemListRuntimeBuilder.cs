using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "ItemListRuntimeBuilder", menuName = "RuntimeBuilder/ItemListRuntimeBuilder")]
    public class ItemListRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] ItemCollectionDefaultData _itemList;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<ItemListRuntimeData>(id, out var dummy)) return;

            var data = new ItemListRuntimeData(_itemList);
            repository.RegisterData(id, data);
        }
    }
}
