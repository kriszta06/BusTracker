using BusTracker.Core.Models;

namespace BusTracker.Core.Services;

public class StopLocator
{
    private const double StopProximityThreshold = 30;
    public StopLocation Locate(GPSCoordinate gps, IReadOnlyList<BusStop> tripStops)
    {
        if(tripStops.Count < 2)
        {
            throw new ArgumentException("At least two stops are required to locate the bus.", nameof(tripStops));
        }
        
        var stops = tripStops.OrderBy(s => s.Sequence).ToList();
        var nearestIndex = 0;
        var nearestDistance = double.MaxValue;
        for(var i = 0; i < stops.Count; i++)
        {
            var d = Distance(gps, stops[i]);
            if(d < nearestDistance)
            {
                nearestDistance = d;
                nearestIndex = i;
            }
        }

        if(nearestDistance < StopProximityThreshold)
        {
            var next = nearestIndex + 1 < stops.Count ? stops[nearestIndex + 1] : null;
            return new StopLocation(stops[nearestIndex], next, true);
        }
        
        var bestIndex = 0; 
        var bestDetour = double.MaxValue;
        for(var i = 0; i < stops.Count - 1; i++)
        {
            var detour = Distance(gps, stops[i]) + Distance(gps, stops[i + 1]) - Distance(stops[i], stops[i + 1]);
            if(detour < bestDetour)
            {
                bestDetour = detour;
                bestIndex = i;
            }
        }
        return new StopLocation(stops[bestIndex], stops[bestIndex + 1], false);
    }

    private static double Distance(GPSCoordinate gps, BusStop stop)
    {
        return GeoCalculator.DistanceBetweenCoordinates(gps.Latitude, gps.Longitude, stop.Latitude, stop.Longitude);
    }

    private static double Distance(BusStop stop1, BusStop stop2)
    {
        return GeoCalculator.DistanceBetweenCoordinates(stop1.Latitude, stop1.Longitude, stop2.Latitude, stop2.Longitude);
    }
}
