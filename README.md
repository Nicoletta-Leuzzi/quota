# Quota

**Quota** is an open-source .NET library for splitting shared expenses and recurring bills among flatmates and small groups.

> Status: early development. APIs will change.

## Goals

- **Flexible splits**: equal, by percentage, by shares, or by exact amount
- **Cent-exact rounding**: amounts are handled in integer cents, so no cent is ever lost or created
- **Debt simplification**: compute the minimum number of transfers needed to settle a group
- **Recurring bills**: monthly or bimonthly bills, with pro-rata splitting for people who join or leave mid-period
- **Import/export**: CSV and JSON

## Project structure

| Path | Description |
|---|---|
| `src/Quota.Core` | Core library |
| `src/Quota.Cli` | Command-line tool |
| `tests/Quota.Core.Tests` | Unit tests (xUnit) |

A client-side Blazor WebAssembly playground is planned.

## Getting started

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet build
dotnet test
dotnet run --project src/Quota.Cli -- split 100.00 3
```

## Roadmap

- [x] Equal split with cent-exact rounding
- [ ] Percentage, shares and exact-amount splits
- [ ] Group ledger and balances
- [ ] Debt simplification (minimum transfers)
- [ ] Recurring bills with pro-rata splitting
- [ ] CSV / JSON import and export
- [ ] Blazor WebAssembly playground
- [ ] NuGet package and CI

## License

[MIT](LICENSE) © 2026 Nicoletta Leuzzi
