namespace Encel.Settings
{
    public interface IAppSettings
    {
        string this[string key] { get; }
    }
}