using BusTracker.Core;

namespace BusTracker.Tests;

public class GeoCalculatorTests
{

    [Fact]
    public void SamePoint_ShouldReturnZero()
    {
        var v = GeoCalculator.DistanceBetweenCoordinates(45.67, 23.45, 45.67, 23.45);
        Assert.Equal(0, v, 3);
    }

    [Fact]
    public void OneDegreeLatitude_ShouldReturnApprox111km()
    {
        var v = GeoCalculator.DistanceBetweenCoordinates(0, 0, 1, 0);
        Assert.InRange(v, 110000, 114000);
    }

    [Fact]
    public void EqualDistance_ShouldReturnSameValue()
    {
        var v1 = GeoCalculator.DistanceBetweenCoordinates(0, 0, 1, 1);
        var v2 = GeoCalculator.DistanceBetweenCoordinates(1, 1, 0, 0);
        Assert.Equal(v1, v2, 3);
    }
}
