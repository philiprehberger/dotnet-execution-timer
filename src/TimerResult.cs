namespace Philiprehberger.ExecutionTimer;

/// <summary>
/// Represents the result of a timed operation, including label, elapsed time, and any nested child results.
/// </summary>
/// <param name="Label">The descriptive label for the timed operation.</param>
/// <param name="Elapsed">The total elapsed time of the operation.</param>
/// <param name="ElapsedMilliseconds">The total elapsed time in milliseconds.</param>
/// <param name="Children">Child timing results from nested scopes.</param>
public record TimerResult(
    string Label,
    TimeSpan Elapsed,
    long ElapsedMilliseconds,
    IReadOnlyList<TimerResult> Children);
