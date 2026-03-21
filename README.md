# Philiprehberger.ExecutionTimer

[![CI](https://github.com/philiprehberger/dotnet-execution-timer/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-execution-timer/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.ExecutionTimer.svg)](https://www.nuget.org/packages/Philiprehberger.ExecutionTimer)
[![License](https://img.shields.io/github/license/philiprehberger/dotnet-execution-timer)](LICENSE)

Measure execution time of code blocks with structured results, nested scopes, and ILogger integration.

## Installation

```bash
dotnet add package Philiprehberger.ExecutionTimer
```

## Usage

```csharp
using Philiprehberger.ExecutionTimer;

var result = ExecutionTimer.Measure("my-operation", () =>
{
    // code to measure
});

Console.WriteLine($"{result.Label}: {result.ElapsedMilliseconds}ms");
```

### Basic Timing

```csharp
using Philiprehberger.ExecutionTimer;

using var scope = ExecutionTimer.Start("database-query");
// perform work
scope.Dispose();

Console.WriteLine($"{scope.Label}: {scope.Elapsed.TotalMilliseconds}ms");
```

### Nested Scopes

```csharp
using Philiprehberger.ExecutionTimer;

using var parent = ExecutionTimer.Start("pipeline");

using (var step1 = parent.StartChild("step-1"))
{
    // first step
}

using (var step2 = parent.StartChild("step-2"))
{
    // second step
}

var result = parent.Result;
foreach (var child in result.Children)
{
    Console.WriteLine($"  {child.Label}: {child.ElapsedMilliseconds}ms");
}
```

### Async Timing

```csharp
using Philiprehberger.ExecutionTimer;

var result = await ExecutionTimer.MeasureAsync("http-call", async () =>
{
    await httpClient.GetAsync("https://example.com");
});

Console.WriteLine($"{result.Label}: {result.ElapsedMilliseconds}ms");
```

## API

### `ExecutionTimer`

| Method | Description |
|--------|-------------|
| `Start(string label)` | Start a new timing scope that measures until disposed |
| `Measure(string label, Action action)` | Measure a synchronous action and return the result |
| `Measure<T>(string label, Func<T> func)` | Measure a synchronous function and return its value |
| `MeasureAsync(string label, Func<Task> action)` | Measure an async action and return the result |
| `MeasureAsync<T>(string label, Func<Task<T>> func)` | Measure an async function and return its value |

### `TimerScope`

| Member | Description |
|--------|-------------|
| `Label` | The descriptive label for the scope |
| `Elapsed` | The elapsed `TimeSpan` since creation |
| `ElapsedMilliseconds` | The elapsed time in milliseconds |
| `StartChild(string label)` | Create a nested child timing scope |
| `Result` | Get the `TimerResult` with all children |
| `Dispose()` | Stop the timer |

### `TimerResult`

| Property | Type | Description |
|----------|------|-------------|
| `Label` | `string` | The descriptive label |
| `Elapsed` | `TimeSpan` | Total elapsed time |
| `ElapsedMilliseconds` | `long` | Total elapsed time in milliseconds |
| `Children` | `IReadOnlyList<TimerResult>` | Nested child results |

### `LoggerExtensions`

| Method | Description |
|--------|-------------|
| `TimeOperation(this ILogger logger, string label)` | Log start and elapsed time on dispose |

## Development

```bash
dotnet build src/Philiprehberger.ExecutionTimer.csproj --configuration Release
```

## License

MIT
