namespace BusTracker.Tests;

using BusTracker.Core.Models;
using BusTracker.Core.Services;
public class StopLocatorTests
{
    [Fact]
    public void LessThanTwoStops_ShouldThrowArgumentException()
    {
        var locator = new StopLocator();
        var gps = new GPSCoordinate("bus1", 0, 0, DateTime.UtcNow);
        var stops = new List<BusStop> { new BusStop("stop1", "Stop 1", 0, 0, 1, TimeSpan.Zero) };

        Assert.Throws<ArgumentException>(() => locator.Locate(gps, stops));
    }

    [Fact]
    public void AtFirstStop_ShouldReturnFirstStopAsCurrent()
    {
        var locator = new StopLocator();
        var gps = new GPSCoordinate("bus1", 0, 0, DateTime.UtcNow);

        var stops = new List<BusStop>
        {
            new BusStop("stop1", "Stop 1", 0, 0, 1, TimeSpan.Zero),
            new BusStop("stop2", "Stop 2", 0.0001, 0.0001, 2, TimeSpan.Zero)
        };

        var result = locator.Locate(gps, stops);

        Assert.Equal(stops[0], result.Current);
        Assert.Equal(stops[1], result.Next);
        Assert.True(result.IsAtStop);
    }

    [Fact]
    public void BetweenStops_ShouldReturnNearestStopAsCurrent()
    {
        var locator = new StopLocator();

        var gps = new GPSCoordinate(
            "bus1", 0.00005, 0.00005, DateTime.UtcNow);

        var stops = new List<BusStop>
        {
            new BusStop("stop1", "Stop 1", -0.0003, -0.0003, 1, TimeSpan.Zero),
            new BusStop("stop2", "Stop 2", 0.0004, 0.0004, 2, TimeSpan.Zero)
        };

        var result = locator.Locate(gps, stops);

        Assert.Equal(stops[0], result.Current);
        Assert.Equal(stops[1], result.Next);
        Assert.False(result.IsAtStop);
    }

    [Fact]
    public void AtStopWithNextStop_ShouldReturnNextStop()
    {
        var locator = new StopLocator();
        var gps = new GPSCoordinate("bus1", 0, 0, DateTime.UtcNow);

        var stops = new List<BusStop>
        {
            new BusStop("stop1", "Stop 1", 0, 0, 1, TimeSpan.Zero),
            new BusStop("stop2", "Stop 2", 0.0001, 0.0001, 2, TimeSpan.Zero),
            new BusStop("stop3", "Stop 3", 0.0002, 0.0002, 3, TimeSpan.Zero)
        };

        var result = locator.Locate(gps, stops);

        Assert.Equal(stops[0], result.Current);
        Assert.Equal(stops[1], result.Next);
        Assert.True(result.IsAtStop);
    }

    [Fact]
    public void AtLastStop_ShouldReturnNullAsNext()
    {
        var locator = new StopLocator();
        var gps = new GPSCoordinate("bus1", 0.0002, 0.0002, DateTime.UtcNow);

        var stops = new List<BusStop>
        {
            new BusStop("stop1", "Stop 1", 0, 0, 1, TimeSpan.Zero),
            new BusStop("stop2", "Stop 2", 0.0001, 0.0001, 2, TimeSpan.Zero),
            new BusStop("stop3", "Stop 3", 0.0002, 0.0002, 3, TimeSpan.Zero)
        };

        var result = locator.Locate(gps, stops);

        Assert.Equal(stops[2], result.Current);
        Assert.Null(result.Next);
        Assert.True(result.IsAtStop);
}
}
