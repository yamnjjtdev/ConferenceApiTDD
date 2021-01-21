using System;
using System.Collections.Generic;
using System.Text;
using ConferenceApiTDD.Lib;
using Xunit;

namespace ConferenceApiTDD.Test
{
    public class Util_Tests
    {

        [Theory]
        [InlineData("104 December 2013 15:00 - 16:00")]
        [InlineData("04 December 42013 15:00 - 16:00")]
        [InlineData("104 December 2013 15:004")]
        public void Compare_WrongFormattedTargetDate_ThrowsFormatException(string targetDate)
        {
            DateTime dateTime = new DateTime(2013, 12, 04, 12, 30, 00);
            Action actual = () => DateComparer.IsDateTimeWithin(dateTime, targetDate);

            Assert.Throws<FormatException>(actual);
        }

        [Theory]
        [InlineData(2013, 12, 04, 12, 30, "04 December 2013 15:00 - 16:00")]
        [InlineData(2013, 12, 04, 12, 30, "04 September 2013 15:00 - 16:00")]
        [InlineData(2013, 12, 04, 12, 30, "04 December 2014 12:00 - 16:00")]
        [InlineData(2014, 12, 04, 12, 30, "04 December 2013 15:00 - 16:00")]
        [InlineData(2013, 12, 05, 15, 30, "04 December 2013 15:00 - 16:00")]
        public void Compare_NotWithinDateRange_ReturnsFalse(
            int year, int month, int day, int hour, int minute, string targetDate)
        {
            var dateToCheck = new DateTime(year, month, day, hour, minute, 00);
            Assert.False(DateComparer.IsDateTimeWithin(dateToCheck, targetDate));
        }

        [Theory]
        [InlineData(2013, 12, 04, 15, 30, "04 December 2013 15:00 - 16:00")]
        [InlineData(2013, 9, 04, 15, 30, "04 September 2013 15:00 - 16:00")]
        [InlineData(2013, 12, 04, 12, 30, "04 December 2013 12:00 - 16:00")]
        [InlineData(2014, 12, 04, 15, 30, "04 December 2014 15:00 - 16:00")]
        [InlineData(2013, 12, 05, 15, 30, "05 December 2013 15:00 - 16:00")]
        public void Compare_WithinDateRange_ReturnsTrue(
            int year, int month, int day, int hour, int minute, string targetDate)
        {
            var dateToCheck = new DateTime(year, month, day, hour, minute, 00);
            Assert.True(DateComparer.IsDateTimeWithin(dateToCheck, targetDate));
        }
    }
}
