namespace BusTracker.Core;

public static class GeoCalculator
{
    public static double DistanceBetweenCoordinates(double lat1, double lon1, double lat2, double lon2)
    {
        var distanceLat = (lat2 - lat1) * Math.PI / 180.0;
        var distanceLon = (lon2 - lon1) * Math.PI / 180.0;

        var lat1Rad = lat1 * Math.PI / 180.0;
        var lat2Rad = lat2 * Math.PI / 180.0;

        var a = Math.Sin(distanceLat / 2) * Math.Sin(distanceLat / 2) +
                Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Sin(distanceLon / 2) * Math.Sin(distanceLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        const double d = 6371e3;

        return d * c;

    }
}
