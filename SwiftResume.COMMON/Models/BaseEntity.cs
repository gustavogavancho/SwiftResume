namespace SwiftResume.COMMON.Models;

public class BaseEntity : IDisposable
{
    private bool _isDiposed;

    public int Id { get; set; }

    public void Dispose()
    {
        if (!_isDiposed)
        {
            this._isDiposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
