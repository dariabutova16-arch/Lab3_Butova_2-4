namespace StudentCabinet.Services;

public interface IAppStateService
{
    string DraftFullName { get; set; }
    string DraftGroup { get; set; }
    double DraftScore { get; set; }
    void Save();
}