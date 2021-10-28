using System;
using Xunit;
using FluentAssertions;

namespace SpotHero_Backend_Challenge.Tests.UnitTests
{
    public class Test_VerifiedParkingRate
    {
        [Theory]
        [InlineData("mon", 9, 00, 15, 00, 1000)] // 3-letter day of week OR ["tues", "thurs"]
        [InlineData("tue", 9, 00, 15, 00, 1000)]
        [InlineData("tues", 9, 00, 15, 00, 1000)]
        [InlineData("wed", 9, 00, 15, 00, 1000)]
        [InlineData("thu", 9, 00, 15, 00, 1000)]
        [InlineData("thurs", 9, 00, 15, 00, 1000)]
        [InlineData("fri", 9, 00, 15, 00, 1000)]
        [InlineData("sat", 9, 00, 15, 00, 1000)]
        [InlineData("sun", 9, 00, 15, 00, 1000)]
        [InlineData("mon", 0, 00, 23, 59, 1000)]
        public void ValidInputs(string dayOfWeek, int startHour, int startMinute, int endHour, int endMinute, int price)
        {
            Action instantiation = () => new VerifiedParkingRate(dayOfWeek, startHour, startMinute, endHour, endMinute, TimeZoneInfo.Local, price);
            instantiation.Should().NotBeNull();
        }

        [Theory]
        [InlineData("monday", 9, 00, 15, 00, 1000)] // 3-letter day of week OR ["tues", "thurs"]
        [InlineData("monkey", 9, 00, 15, 00, 1000)]
        [InlineData("mon", -1, 00, 15, 00, 1000)]
        [InlineData("mon", 9, 00, 25, 00, 1000)]
        [InlineData("mon", 9, -1, 15, 00, 1000)]
        [InlineData("mon", 9, 00, 8, 00, 1000)]
        [InlineData("mon", 9, 00, 9, 00, 1000)]
        [InlineData("mon", 9, 60, 15, 00, 1000)]
        [InlineData("mon", 9, 00, 15, 60, 1000)]
        [InlineData("mon", 9, 00, 15, 00, 0)]
        [InlineData("mon", 9, 00, 15, 00, -1)]
        public void InvalidInputs(string dayOfWeek, int startHour, int startMinute, int endHour, int endMinute, int price)
        {
            Action instantiation = () => new VerifiedParkingRate(dayOfWeek, startHour, startMinute, endHour, endMinute, TimeZoneInfo.Local, price);
            instantiation.Should().Throw<ArgumentException>();
        }
    }
}
