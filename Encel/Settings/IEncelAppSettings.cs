namespace Encel.Settings
{
    public interface IEncelAppSettings : IAppSettings
    {
        string FileExtension { get; set; }
        bool EnableContentCaching { get; set; }
        string RootPath { get; set; }
    }
}