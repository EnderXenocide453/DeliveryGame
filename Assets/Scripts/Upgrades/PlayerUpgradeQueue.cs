using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(Storage))]
public class PlayerUpgradeQueue : MonoBehaviour
{
    [SerializeField] private PlayerUpgrade[] upgrades;
    private int _currentID;

    private PlayerMovement _target;
    private Storage _targetStorage;

    public BaseUpgrade CurrentUpgrade { get => _currentID >= upgrades.Length ? null : upgrades[_currentID]; }

    public event BaseUpgrade.UpgradeEventHandler onUpgraded;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (upgrades.Length == 0)
            return;

        _target = GetComponent<PlayerMovement>();
        _targetStorage = GetComponent<Storage>();

        for (int i = 1; i < upgrades.Length; i++) {
            upgrades[i - 1].nextUpgrade = upgrades[i];
            upgrades[i].onUpgraded += OnUpgraded;
            upgrades[i].SetTarget(_target, _targetStorage);
        }

        upgrades[0].onUpgraded += OnUpgraded;
        upgrades[0].SetTarget(_target, _targetStorage);
    }

    private void OnUpgraded()
    {
        _currentID++;

        onUpgraded?.Invoke();
    }

    [System.Serializable]
    private class PlayerUpgrade : BaseUpgrade
    {
        [SerializeField] int storageCapacity;
        [SerializeField] float speedModifier;

        private PlayerMovement _target;
        private Storage _targetStorage;

        public void SetTarget(PlayerMovement target, Storage storage)
        {
            _target = target;
            _targetStorage = storage;
        }

        protected override void PostUpgrade()
        {
            _target.speedModifier = speedModifier;
            _targetStorage.MaxCount = storageCapacity;
        }

        protected override void PreUpgrade() { }
    }
}