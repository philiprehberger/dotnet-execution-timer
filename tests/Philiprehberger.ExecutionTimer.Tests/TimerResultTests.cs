using Xunit;
using Philiprehberger.ExecutionTimer;

namespace Philiprehberger.ExecutionTimer.Tests;

public class TimerResultTests
{
    [Fact]
    public void TimerResult_StoresLabel()
    {
        var result = new TimerResult("test", TimeSpan.FromMilliseconds(100), 100, Array.Empty<TimerResult>());

        Assert.Equal("test", result.Label);
    }

    [Fact]
    public void TimerResult_StoresElapsed()
    {
        var elapsed = TimeSpan.FromMilliseconds(250);
        var result = new TimerResult("test", elapsed, 250, Array.Empty<TimerResult>());

        Assert.Equal(elapsed, result.Elapsed);
        Assert.Equal(250, result.ElapsedMilliseconds);
    }

    [Fact]
    public void TimerResult_StoresChildren()
    {
        var child = new TimerResult("child", TimeSpan.FromMilliseconds(50), 50, Array.Empty<TimerResult>());
        var result = new TimerResult("parent", TimeSpan.FromMilliseconds(100), 100, new[] { child });

        Assert.Single(result.Children);
        Assert.Equal("child", result.Children[0].Label);
    }
}
