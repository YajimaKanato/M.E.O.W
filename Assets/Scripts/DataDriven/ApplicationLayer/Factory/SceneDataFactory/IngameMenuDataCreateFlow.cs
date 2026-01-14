using DataDriven;
using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "IngameMenuDataCreateFlow", menuName = "DataFlow/IngameMenuDataCreateFlow")]
    public class IngameMenuDataCreateFlow : SceneDataCreateFlow
    {
        [Header("DefaultData")]
        [SerializeField] MenuDefaultData _menu;
        [Header("Config")]
        [SerializeField] ConfigDefaultData _config;
        [SerializeField] BGMDefaultData _bgm;
        [SerializeField] SEDefaultData _se;
        [SerializeField] TextConfigDefaultData _text;
        [Header("ItemCollection")]
        [SerializeField] ItemCollectionDefaultData _itemList;
        [Header("Log")]
        [SerializeField] LogDefaultData _log;
        [Header("Info")]
        [SerializeField] InfoDefaultData _info;

        public override void CreateSceneData(RuntimeDataRepository repository)
        {
            _repository = repository;
            DataCreate(DataID.Menu, _menu, data => new MenuRuntimeData(data));
            //Config
            DataCreate(DataID.Config, _config, data => new ConfigRuntimeData(data));
            DataCreate(DataID.BGM, _bgm, data => new BGMRuntimeData(data));
            DataCreate(DataID.SE, _se, data => new SERuntimeData(data));
            DataCreate(DataID.Text, _text, data => new TextConfigRuntimeData(data));
            //ItemCollection
            DataCreate(DataID.ItemList, _itemList, data => new ItemListRuntimeData(data));
            //Log
            DataCreate(DataID.Log, _log, data => new LogRuntimeData(data));
            //Info
            DataCreate(DataID.Info, _info, data => new InfoRuntimeData(data));
        }
    }
}
