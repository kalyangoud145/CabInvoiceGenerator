using NUnit.Framework;
using CabInvoiceGenerator;
namespace CabInvoiceUnitTest
{
    /// <summary>
    /// NUnit Testing class
    /// </summary>
    public class CabInvoiceNUnitTest
    {
        InvoiceGenerator invoiceGenerator = null;
        /// TC 1
        /// <summary>
        /// Given the distance and time when normal ride should return total fare.
        /// </summary>
        [Test]
        public void GivenDistanceAndTime_WhenNormalRide_ShouldReturnTotalFare()
        {
            double expected = 25;
            double distance = 2.0;
            int time = 5;
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double fare = invoiceGenerator.CalculateFare(distance, time);
            Assert.AreEqual(expected, fare);
        }
        /// TC 2 
        /// <summary>
        /// Given the distance and time when normal ride should return fare.
        /// </summary>
        [Test]
        public void GivenDistanceAndTime_WhenNormalRide_ShouldReturnFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2;
            int time = 2;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 22;
            Assert.AreEqual(expected, fare);
        }
        /// TC 3
        /// <summary>
        /// Given Invalid time  When Normal Ride should return exception invalid time
        /// </summary>
        [Test]
        public void GivenInvalidTime_WhenNormalRide_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = 50;
                int time = -20;
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_TIME);
            }
        }
        /// TC 4
        /// <summary>
        /// Given Invalid time should return exception invalid distance
        /// </summary>
        [Test]
        public void GivenInvalidDistance_WhenNormalRideShould_Return_CabInvoiceException()
        {
            try
            {
                double distance = -5;
                int time = 20;
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_DISTANCE);
            }
        }
        /// TC 5
        /// <summary>
        /// Given invalid ride type should return exception invalid ride type
        /// </summary>
        [Test]
        public void GivenInvalidRideType_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = 11;
                int time = 20;
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE);
            }
        }
        /// TC 6
        /// <summary>
        /// Given Invalid UserId When Getting UserRide should Return CabInvoiceException invalid user id
        /// </summary>
        [Test]
        public void GivenInvalidUserId_WhenGettingUserRideshould_ReturnCabInvoiceException()
        {
            try
            {
                RideRepository rideRepository = new RideRepository();
                Ride[] actual = rideRepository.GetRides("InvalidUserID");
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_USER_ID);
            }
        }
        /// TC 7
        /// <summary>
        /// Givens the distance and time for multiple rides when normal ride 
        /// should return aggregatefare, number of rides ,average fare.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeForMultipleRides_WhenNormalRide_ShouldReturnAggregateFare_NumberOfRides_AverageFare()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                new Ride(4.0, 4),
                new Ride(5.0, 3)
            };
            double expected = 162;
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            double actual = summary.totalFare;
            int totalNumberOfRides = summary.numberOfRides;
            double averageFareForRide = summary.averageFare;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(5, totalNumberOfRides);
            Assert.AreEqual(32.4, averageFareForRide);
        }
        /// TC 8
        /// <summary>
        /// Givens the user id and array of rides when normal ride should
        /// return array of rides for particular user id.
        /// </summary>
        [Test]
        public void GivenUserId_AndArrayOfRides_WhenNormalRide_ShouldReturnArrayOfRidesForParticularUserId()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                new Ride(4.0, 4),
                new Ride(5.0, 3)
            };
            string userId = "123";
            RideRepository rideRepository = new RideRepository();
            rideRepository.AddRide(userId, rides);
            Ride[] actual = rideRepository.GetRides(userId);
            Assert.AreEqual(rides, actual);
        }
        /// TC 9
        /// <summary>
        /// Given Ride Values When Premium Ride Should Return AggregateFare, NumberOfRides, AverageFare 
        /// </summary>
        [Test]
        public void GivenRideValues_WhenPremiumRide_ShouldReturnAggregateFare_NumberOfRides_AverageFare()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                new Ride(4.0, 4),
                new Ride(5.0, 3)
            };
            double expected = 252;
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            double actual = summary.totalFare;
            int totalNumberOfRides = summary.numberOfRides;
            double averageFareForRide = summary.averageFare;
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(5, totalNumberOfRides);
            Assert.AreEqual(50.4, averageFareForRide);
        }
        /// TC 10
        /// <summary>
        /// Given null ride when adding to ride list should return exception 
        /// </summary>
        [Test]
        public void GivenNullRide_WhenTryingToAddToRidelistShould_ReturnCabInvoiceException()
        {
            try
            {
                Ride[] rides =
                {
                    new Ride(5, 20),
                    null,
                    new Ride(2, 10)
                };
                RideRepository rideRepository = new RideRepository();
                rideRepository.AddRide("111", rides);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.NULL_RIDES);
            }
        }
        /// TC 11
        /// <summary>
        ///  Given Ride Values When Invoice Summary Is Provided Should Return Enhanced Invoice Summary
        /// </summary>
        [Test]
        public void Given5RideValuesAnd_WhenInvoiceSummaryIsProvidedShould_ReturnEnhancedInvoiceSummary()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                new Ride(4.0, 4),
                new Ride(5.0, 3)
            };
            InvoiceSummary expected = new InvoiceSummary(5, 162);
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            Assert.AreEqual(summary, expected);
        }
        /// TC 12
        /// <summary>
        /// Given null ride value when calculating invoice summary should throw exception
        /// </summary>
        [Test]
        public void GivenNullRideValues_WhenPremiumRide_ShouldReturnException()
        {
            try
            {
                Ride[] rides =
                {
                    new Ride(5, 20),
                    null,
                    new Ride(2, 10)
                };
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
                InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
                double actual = summary.totalFare;
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.NULL_RIDES);
            }
        }
    }
}