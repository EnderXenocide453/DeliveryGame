using UnityEngine;

public static class GlobalValueHandler
{
    private static int _cash;

    public static int Cash
    {
        get => _cash;
        set
        {
            if (value - _cash > 0)
                SoundsManager.PlaySound(SoundsManager.instance.getCashSound, 1.5f);

            _cash = value;
            onCashChanged?.Invoke();
        }
    }

    public static Sprite ApplyIcon { get => Resources.Load<Sprite>("Icons/ApplyIcon"); }
    public static Sprite DenyIcon { get => Resources.Load<Sprite>("Icons/DenyIcon"); }

    public delegate void ValueEventHandler();
    public static event ValueEventHandler onCashChanged;
}
