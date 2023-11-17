public static class GlobalValueHandler
{
    private static int _cash;

    public static int Cash
    {
        get => _cash;
        set
        {
            _cash = value;
            onCashChanged?.Invoke();
        }
    }

    public delegate void ValueEventHandler();
    public static event ValueEventHandler onCashChanged;
}
