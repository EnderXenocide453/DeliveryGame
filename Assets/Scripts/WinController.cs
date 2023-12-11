using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinController : MonoBehaviour
{
    public static WinController instance;

    private static bool _playerUpgraded;
    private static int _couriersUpgraded;
    private static int _storagesUpgraded;
    private static bool _truckUpgraded;

    [SerializeField] int storagesCount;
    [SerializeField] UnityEvent winEvent;

    void Start()
    {
        if (instance) {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public static void OnCourierMaxUpgrade()
    {
        _couriersUpgraded++;
        CheckWinConditions();
    }

    public static void OnPlayerMaxUpgrade()
    {
        _playerUpgraded = true;
        CheckWinConditions();
    }

    public static void OnStorageMaxUpgrade()
    {
        _storagesUpgraded++;
        CheckWinConditions();
    }

    public static void OnTruckMaxUpgrade()
    {
        _truckUpgraded = true;
        CheckWinConditions();
    }

    private static void CheckWinConditions()
    {
        if (_playerUpgraded && _couriersUpgraded == CourierManager.MaxCount && _storagesUpgraded == instance.storagesCount && _truckUpgraded)
            instance.winEvent?.Invoke();
    }
}
