using Xunit;
using Philiprehberger.ExecutionTimer;

namespace Philiprehberger.ExecutionTimer.Tests;

public class ExecutionTimerTests
{
    [Fact]
    public void Start_ReturnsTimerScopeWithLabel()
    {
        using var scope = ExecutionTimer.Start("test");

        Assert.Equal("test", scope.Label);
    }

    [Fact]
    public void Measure_Action_ReturnsTimerResultWithLabel()
    {
        var result = ExecutionTimer.Measure("action", () => { });

        Assert.Equal("action", result.Label);
        Assert.True(result.ElapsedMilliseconds >= 0);
    }

    [Fact]
    public void Measure_Func_ReturnsValue()
    {
        var value = ExecutionTimer.Measure("func", () => 42);

        Assert.Equal(42, value);
    }

    [Fact]
    public async Task MeasureAsync_Action_ReturnsTimerResult()
    {
        var result = await ExecutionTimer.MeasureAsync("async-action", () => Task.CompletedTask);

        Assert.Equal("async-action", result.Label);
    }

    [Fact]
    public async Task MeasureAsync_Func_ReturnsValue()
    {
        var value = await ExecutionTimer.MeasureAsync("async-func", () => Task.FromResult(99));

        Assert.Equal(99, value);
    }
}
