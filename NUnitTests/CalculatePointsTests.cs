using Web;

namespace NUnitTests;

[TestFixture]
public class CalculatePointsTests
{
    private const int Spring = 3;
    private const int Summer = 6;
    private const int Winter = 12;
    
    [Test]
    [TestCase(Spring, 1, 2)]
    [TestCase(Spring, 2, 6)]
    [TestCase(Spring, 3, 14)]
    
    [TestCase(Summer, 1, 2)]
    [TestCase(Summer, 2, 6)]
    [TestCase(Summer, 3, 14)]
    
    [TestCase(Winter, 1, 2)]
    [TestCase(Winter, 2, 6)]
    [TestCase(Winter, 3, 14)]
    
    public void Test(int month, int day, int expectedPoints)
    {
        // Prepare
        var date = new DateTime(2023, month, day);
        
        // Action
        var actualPoints = PointsHelpers.Calculate(date);
        
        // Test
        Assert.That(actualPoints, Is.EqualTo(expectedPoints));
    }
}