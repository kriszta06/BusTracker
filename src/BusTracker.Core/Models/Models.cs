namespace BusTracker.Core.Models;

public record GPSCoordinate(string VehicleID, double Latitude, double Longitude, DateTime Timestamp);

public record BusStop(string StopID, string Name, double Latitude, double Longitude, int Sequence, TimeSpan ScheduledArrivalTime);

public record StopLocation(BusStop Current, BusStop? Next, bool IsAtStop);
