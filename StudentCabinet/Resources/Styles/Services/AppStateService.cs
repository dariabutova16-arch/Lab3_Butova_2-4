namespace StudentCabinet.Services;

public class AppStateService : IAppStateService
{
    private string _fullName;
    private string _group;
    private double _score;

    public AppStateService()
    {
        _fullName = Preferences.Get("draft_fullname", string.Empty);
        _group = Preferences.Get("draft_group", string.Empty);
        _score = Preferences.Get("draft_score", 0.0);
        System.Diagnostics.Debug.WriteLine("[State] ПРОЧИТАНО при старті: '" + _fullName + "' / '" + _group + "' / " + _score);
    }

    public string DraftFullName
    {
        get { return _fullName; }
        set
        {
            _fullName = value ?? string.Empty;
            Preferences.Set("draft_fullname", _fullName);
        }
    }

    public string DraftGroup
    {
        get { return _group; }
        set
        {
            _group = value ?? string.Empty;
            Preferences.Set("draft_group", _group);
        }
    }

    public double DraftScore
    {
        get { return _score; }
        set
        {
            _score = value;
            Preferences.Set("draft_score", _score);
        }
    }

    public void Save()
    {
        Preferences.Set("draft_fullname", _fullName);
        Preferences.Set("draft_group", _group);
        Preferences.Set("draft_score", _score);
        System.Diagnostics.Debug.WriteLine("[State] Стан збережено");
    }
}