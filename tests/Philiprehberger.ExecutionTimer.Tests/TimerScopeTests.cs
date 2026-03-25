using Xunit;
using Philiprehberger.ExecutionTimer;

namespace Philiprehberger.ExecutionTimer.Tests;

public class TimerScopeTests
{
    [Fact]
    public void Dispose_StopsTimer()
    {
        var scope = ExecutionTimer.Start("stop-test");
        scope.Dispose();
        var elapsed1 = scope.ElapsedMilliseconds;

        Thread.Sleep(50);
        var elapsed2 = scope.ElapsedMilliseconds;

        Assert.Equal(elapsed1, elapsed2);
    }

    [Fact]
    public void StartChild_AddsChildToResult()
    {
        using var parent = ExecutionTimer.Start("parent");
        using var child = parent.StartChild("child");

        var result = parent.Result;

        Assert.Single(result.Children);
        Assert.Equal("child", result.Children[0].Label);
    }

    [Fact]
    public void Result_ContainsCorrectLabel()
    {
        using var scope = ExecutionTimer.Start("my-label");

        Assert.Equal("my-label", scope.Result.Label);
    }
}
