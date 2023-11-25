using System;
using System.IO;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] string savePath;

    public static bool startNewGame;

    private PlayerUpgradeQueue _playerUpgrades;
    private StorageUpgradeQueue[] _storageUpgrades;
    private TruckUpgradeQueue _truckUpgrades;

    private void Start()
    {
        Init();

        if (startNewGame)
            return;

        LoadGame();
    }

    private void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, fileName);
#else
        savePath = Path.Combine(Application.dataPath, fileName);
#endif

        _playerUpgrades = FindObjectOfType<PlayerUpgradeQueue>();
        _storageUpgrades = FindObjectsOfType<StorageUpgradeQueue>(true);
        _truckUpgrades = FindObjectOfType<TruckUpgradeQueue>();
    }

    [ContextMenu("Load")]
    private void LoadGame()
    {
        if (!File.Exists(savePath))
            return;

        try {
            string json = File.ReadAllText(savePath);
            var save = JsonUtility.FromJson<SaveStruct>(json);

            _playerUpgrades.UpgradeQueue.UpgradeTo(save.playerLevel);
            _truckUpgrades.UpgradeQueue.UpgradeTo(save.truckLevel);

            for (int i = 0; i < save.storagesLevels.Length; i++) {
                _storageUpgrades[i].UpgradeQueue.UpgradeTo(save.storagesLevels[i]);
            }

            for (int i = 0; i < save.couriersLevels.Length; i++) {
                CourierManager.instance.AddNewCourier().UpgradeQueue.UpgradeQueue.UpgradeTo(save.couriersLevels[i]);
            }

            GlobalValueHandler.Cash = save.cash;
        }
        catch (Exception e) {
            Debug.LogWarning($"Ошибка загрузки!\n{e}");
        }
    }

    [ContextMenu("Save game")]
    private void SaveGame()
    {
        //Заполнение данных
        SaveStruct save = new SaveStruct();

        save.playerLevel = _playerUpgrades.UpgradeQueue.currentID;
        save.truckLevel = _truckUpgrades.UpgradeQueue.currentID;

        save.storagesLevels = new int[_storageUpgrades.Length];
        for (int i = 0; i < _storageUpgrades.Length; i++) {
            if (_storageUpgrades[i].UpgradeQueue.isLocked) {
                save.storagesLevels[i] = -1;
                continue;
            }

            save.storagesLevels[i] = _storageUpgrades[i].UpgradeQueue.currentID;
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

    private struct SaveStruct
    {
        public int playerLevel;
        public int truckLevel;
        public int[] storagesLevels;
        public int[] couriersLevels;
        public int cash;
    }
}
