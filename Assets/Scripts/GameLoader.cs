using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] string savePath;

    public static bool startNewGame;

    private PlayerUpgradeQueue _playerUpgrades;
    private StorageUpgradeQueue[] _storageUpgrades;
    private Storage[] _storages;
    private BuildArea[] _buildAreas;
    private TruckUpgradeQueue _truckUpgrades;

    private void Start()
    {
        Init();

        if (!startNewGame)
            LoadGame();
    }

    public void QuitToMenu()
    {
        SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        SaveGame();
    }
#endif

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, fileName);
#else
        savePath = Path.Combine(Application.dataPath, fileName);
#endif

        _playerUpgrades = FindObjectOfType<PlayerUpgradeQueue>();

        var buildAreas = FindObjectsOfType<BuildArea>(true);
        _buildAreas = new BuildArea[buildAreas.Length];
        foreach (var area in buildAreas) {
            _buildAreas[area.ID] = area;
        }

        var storageUpgrades = FindObjectsOfType<StorageUpgradeQueue>(true);
        _storageUpgrades = new StorageUpgradeQueue[storageUpgrades.Length];
        _storages = new Storage[storageUpgrades.Length];
        foreach (var upgrade in storageUpgrades) {
            _storageUpgrades[upgrade.ID] = upgrade;
            _storages[upgrade.ID] = upgrade.GetComponent<Storage>();
        }

        _truckUpgrades = FindObjectOfType<TruckUpgradeQueue>();
    }

    [ContextMenu("Load")]
    private void LoadGame()
    {
        if (!File.Exists(savePath))
            return;

        //try {
            string json = File.ReadAllText(savePath);
            var save = JsonUtility.FromJson<SaveStruct>(json);

            _playerUpgrades.UpgradeQueue.UpgradeTo(save.playerLevel);
            _truckUpgrades.UpgradeQueue.UpgradeTo(save.truckLevel);

            for (int i = 0; i < save.buildings.Length; i++) {
                if (save.buildings[i].isBuilded)
                    _buildAreas[i].Build();

                _buildAreas[i].SetCash(save.buildings[i].cash);
            }

            for (int i = 0; i < save.storages.Length; i++) {
                _storageUpgrades[i].UpgradeQueue.UpgradeTo(save.storages[i].level);

                _storages[i].SetGoods(save.storages[i].GetGoods());
            }

            for (int i = 0; i < save.couriersLevels.Length; i++) {
                CourierManager.instance.AddNewCourier().UpgradeQueue.UpgradeQueue.UpgradeTo(save.couriersLevels[i]);
            }

            GlobalValueHandler.Cash = save.cash;
        //}
        //catch (Exception e) {
        //    Debug.LogWarning($"Ошибка загрузки!\n{e}");
        //}
    }

    [ContextMenu("Save game")]
    private void SaveGame()
    {
        //Заполнение данных
        SaveStruct save = new SaveStruct();

        save.playerLevel = _playerUpgrades.UpgradeQueue.currentID;
        save.truckLevel = _truckUpgrades.UpgradeQueue.currentID;

        save.storages = new SaveStorageStruct[_storageUpgrades.Length];
        for (int i = 0; i < _storageUpgrades.Length; i++) {
            save.storages[i] = new SaveStorageStruct();

            if (_storageUpgrades[i].UpgradeQueue.isLocked) {
                save.storages[i].level = -1;
                continue;
            }

            save.storages[i].level = _storageUpgrades[i].UpgradeQueue.currentID;
            save.storages[i].SetGoods(_storages[i].StoredProducts);
        }

        save.buildings = new SaveBuildingStruct[_buildAreas.Length];
        for (int i = 0; i < _buildAreas.Length; i++) {
            save.buildings[i] = new SaveBuildingStruct();
            save.buildings[i].isBuilded = _buildAreas[i].alreadyBuilded;
            save.buildings[i].cash = _buildAreas[i].storedCash;
        }

        save.couriersLevels = new int[CourierManager.Couriers.Count];
        for (int i = 0; i < CourierManager.Couriers.Count; i++) {
            save.couriersLevels[i] = CourierManager.Couriers[i].UpgradeQueue.UpgradeQueue.currentID;
        }

        save.cash = GlobalValueHandler.Cash;

        //Сохранение в файл
        string json = JsonUtility.ToJson(save, true);

        try {
            File.WriteAllText(savePath, json);
        } catch {
            Debug.LogWarning("Ошибка сохранения!");
        }
    }

    [Serializable]
    private struct SaveStruct
    {
        public int playerLevel;
        public int truckLevel;
        public SaveStorageStruct[] storages;
        public SaveBuildingStruct[] buildings;
        public int[] couriersLevels;
        public int cash;
    }

    [Serializable]
    private struct SaveStorageStruct
    {
        public int level;
        public GoodsSaveStruct[] goods;

        public void SetGoods(Dictionary<ProductType, int> goodsDict)
        {
            goods = new GoodsSaveStruct[goodsDict.Count];
            int id = 0;

            foreach (var pair in goodsDict) {
                goods[id] = new GoodsSaveStruct();
                goods[id].type = (int)pair.Key;
                goods[id].count = pair.Value;

                id++;
            }
        }

        public Dictionary<ProductType, int> GetGoods()
        {
            var goodsDict = new Dictionary<ProductType, int>();

            foreach (var product in goods) {
                goodsDict.TryAdd((ProductType)product.type, product.count);
            }

            return goodsDict;
        }

        [Serializable]
        public struct GoodsSaveStruct
        {
            public int type;
            public int count;
        }
    }

    [Serializable]
    private struct SaveBuildingStruct
    {
        public bool isBuilded;
        public int cash;
    }
}
