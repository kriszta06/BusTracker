
# BusTrackingSystem

## Scope

BusTrackingSystem is an application that helps people who travel long distances that cannot be covered with a single bus. In this case, it is essential to know the time details, namely bus delays, in order to make your route more efficient. Thus, BusTrackingSystem was designed to provide real-time information about the movement of the current bus and about whether or not you can switch to another bus at a given stop (because of delays).

As for features, the system gives the user information about the delay of the bus they are on and, simultaneously, two lists: one with the buses the user can switch to at the next stop, and one with the buses they will no longer catch at the next stop.

## Architecture


The attached [diagram](architecture.md) shows the system architecture. BusSimulator replaces a real GPS by producing the bus positions. VehicleTrackingService is the component that coordinates the whole delay calculation system. It uses EtaCalculator to get the minutes until the next stop and the delay relative to the schedule. VehicleRegistry is the memory of the live state of all buses. TransferService, through its `Evaluate` method, provides the list of buses that can be caught and the list of buses that cannot be caught at the next stop.

## Technologies
The system is based on Docker + .NET, using React for the UI and SignalR for the Hub.

## Status
The project is in the design phase.
