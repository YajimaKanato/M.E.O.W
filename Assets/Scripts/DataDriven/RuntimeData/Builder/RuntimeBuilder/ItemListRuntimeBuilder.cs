using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "ItemListRuntimeBuilder", menuName = "RuntimeBuilder/ItemListRuntimeBuilder")]
    public class ItemListRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] ItemCollectionDefaultData _itemList;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<ItemListRuntimeData>(id, out var dummy)) return false;

            var data = new ItemListRuntimeData(_itemList);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
