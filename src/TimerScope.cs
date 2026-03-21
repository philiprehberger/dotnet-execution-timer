using System.Diagnostics;

namespace Philiprehberger.ExecutionTimer;

/// <summary>
/// A disposable scope that measures elapsed time from creation until disposal.
/// Supports nested child scopes for hierarchical timing.
/// </summary>
public class TimerScope : IDisposable
{
    private readonly Stopwatch _stopwatch;
    private readonly List<TimerScope> _children = [];

    /// <summary>
    /// Initializes a new <see cref="TimerScope"/> with the specified label and starts timing immediately.
    /// </summary>
    /// <param name="label">A descriptive label for this timing scope.</param>
    internal TimerScope(string label)
    {
        Label = label;
        _stopwatch = Stopwatch.StartNew();
    }

    /// <summary>
    /// Gets the descriptive label for this timing scope.
    /// </summary>
    public string Label { get; }

    /// <summary>
    /// Gets the elapsed time since the scope was created.
    /// </summary>
    public TimeSpan Elapsed => _stopwatch.Elapsed;

    /// <summary>
    /// Gets the elapsed time in milliseconds since the scope was created.
    /// </summary>
    public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;

    /// <summary>
    /// Gets the <see cref="TimerResult"/> representing the current state of this scope,
    /// including all child results.
    /// </summary>
    public TimerResult Result => new(
        Label,
        Elapsed,
        ElapsedMilliseconds,
        _children.Select(c => c.Result).ToList().AsReadOnly());

    /// <summary>
    /// Creates a nested child timing scope with the specified label.
    /// The child's result is included in this scope's <see cref="Result"/> property.
    /// </summary>
    /// <param name="label">A descriptive label for the child scope.</param>
    /// <returns>A new <see cref="TimerScope"/> that is a child of this scope.</returns>
    public TimerScope StartChild(string label)
    {
        var child = new TimerScope(label);
        _children.Add(child);
        return child;
    }

    /// <summary>
    /// Stops the timer for this scope.
    /// </summary>
    public void Dispose()
    {
        _stopwatch.Stop();
        GC.SuppressFinalize(this);
    }
}
