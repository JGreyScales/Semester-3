# BadScadaEV (C#) – Smelly EV Charging Plaza SCADA/HMI Code for Refactoring

## Run
```bash
dotnet build BadScadaEV.sln
dotnet run --project BadScadaEV
dotnet run --project BadScadaEV.Tests   # optional tiny AAA runner
```

## Student Tasks
1) Identify smells (SRP, DIP, ISP, LoD, DRY) in `BadScadaEV/Program.cs`.
2) Sketch a refactor plan (UML) with interfaces: ICharger, ISitePowerMeter, IPricingService, IProtocolAdapter, ILogger, IHmiPanel, IAlarmSink; Supervisor/Hub; DTOs for HMI.
3) Implement one slice (e.g., extract IProtocolAdapter + inject fake; or extract PricingService).
4) Write two AAA test cases in `BadScadaEV.Tests/TestRunner.cs` (1 unit — load shed policy, 1 integration — session start → HMI + log).

## Notes
- Code intentionally mixes polling, protocol encoding, pricing, HMI, logging, and DB in one place.
- Randomness and time are embedded; replace with abstractions during refactor.
- Deep ‘reach-through’ to anonymous ‘proto’ settings shows Law of Demeter violations.
