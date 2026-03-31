# Changelog

## 0.1.3 (2026-03-31)

- Standardize README to 3-badge format with emoji Support section
- Update CI actions to v5 for Node.js 24 compatibility
- Add GitHub issue templates, dependabot config, and PR template

## 0.1.2 (2026-03-26)

- Add Sponsor badge and fix License link format in README

## 0.1.1 (2026-03-24)

- Add unit tests
- Add test step to CI workflow

## 0.1.0 (2026-03-21)

- Initial release
- `ExecutionTimer` static class with `Start`, `Measure`, and `MeasureAsync` methods
- `TimerScope` with nested child scopes via `StartChild`
- `TimerResult` record with label, elapsed time, and children
- `LoggerExtensions.TimeOperation` for ILogger integration
