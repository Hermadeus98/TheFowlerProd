namespace QRCode
{
    [Address(1, typeof(GameEventAddressCore))]
    public enum GameEventAddressCore : byte
    {
        SetDependancies,
        OnGameStart,
        OnGamePause,
        OnGameUnpause,
    }
}
