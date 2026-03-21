using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Philiprehberger.ExecutionTimer;

/// <summary>
/// Extension methods for <see cref="ILogger"/> that provide timed operation logging.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Starts a timed operation that logs the start and elapsed time on dispose.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="label">A descriptive label for the operation being timed.</param>
    /// <returns>An <see cref="IDisposable"/> that logs the elapsed time when disposed.</returns>
    public static IDisposable TimeOperation(this ILogger logger, string label)
    {
        return new LoggingTimerScope(logger, label);
    }

    private sealed class LoggingTimerScope : IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _label;
        private readonly Stopwatch _stopwatch;

        public LoggingTimerScope(ILogger logger, string label)
        {
            _logger = logger;
            _label = label;
            _stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Starting {Label}", _label);
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.LogInformation("Completed {Label} in {ElapsedMilliseconds}ms", _label, _stopwatch.ElapsedMilliseconds);
        }
    }
}
