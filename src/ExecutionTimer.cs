namespace Philiprehberger.ExecutionTimer;

/// <summary>
/// Provides static methods to measure execution time of code blocks with structured results.
/// </summary>
public static class ExecutionTimer
{
    /// <summary>
    /// Starts a new timing scope with the specified label. Dispose the returned scope to stop timing.
    /// </summary>
    /// <param name="label">A descriptive label for the timing scope.</param>
    /// <returns>A <see cref="TimerScope"/> that measures elapsed time until disposed.</returns>
    public static TimerScope Start(string label)
    {
        return new TimerScope(label);
    }

    /// <summary>
    /// Measures the execution time of a synchronous action.
    /// </summary>
    /// <param name="label">A descriptive label for the timed operation.</param>
    /// <param name="action">The action to measure.</param>
    /// <returns>A <see cref="TimerResult"/> containing the elapsed time.</returns>
    public static TimerResult Measure(string label, Action action)
    {
        using var scope = new TimerScope(label);
        action();
        return scope.Result;
    }

    /// <summary>
    /// Measures the execution time of a synchronous function and returns its result.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="label">A descriptive label for the timed operation.</param>
    /// <param name="func">The function to measure.</param>
    /// <returns>The result of the function.</returns>
    public static T Measure<T>(string label, Func<T> func)
    {
        using var scope = new TimerScope(label);
        return func();
    }

    /// <summary>
    /// Measures the execution time of an asynchronous operation.
    /// </summary>
    /// <param name="label">A descriptive label for the timed operation.</param>
    /// <param name="action">The asynchronous action to measure.</param>
    /// <returns>A <see cref="TimerResult"/> containing the elapsed time.</returns>
    public static async Task<TimerResult> MeasureAsync(string label, Func<Task> action)
    {
        using var scope = new TimerScope(label);
        await action();
        return scope.Result;
    }

    /// <summary>
    /// Measures the execution time of an asynchronous function and returns its result.
    /// </summary>
    /// <typeparam name="T">The return type of the asynchronous function.</typeparam>
    /// <param name="label">A descriptive label for the timed operation.</param>
    /// <param name="func">The asynchronous function to measure.</param>
    /// <returns>The result of the asynchronous function.</returns>
    public static async Task<T> MeasureAsync<T>(string label, Func<Task<T>> func)
    {
        using var scope = new TimerScope(label);
        return await func();
    }
}
