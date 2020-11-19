using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class RideRepository
    {
        // Dictionary for storing the list of rides as value and userid as key
        Dictionary<string, List<Ride>> userRides = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="RideRepository"/> class.
        /// </summary>
        public RideRepository()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }
        /// <summary>
        /// Adds the ride to the dictionary 
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="rides">The rides.</param>
        /// <exception cref="CabInvoiceGenerator.CabInvoiceException">Rides are null</exception>
        public void AddRide(string userId, Ride[] rides)
        {
            // If userid does not exist in the dictionary the ridelist value
            // will be false else true
            bool rideList = this.userRides.ContainsKey(userId);
            try
            {
                // If ridelist is false then the rides list will be added to dictionary
                // With userid as key
                if (!rideList)
                {
                    foreach (Ride ride in rides)
                    {
                        try
                        {                            
                            if (ride == null)
                            {
                                throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
                            }
                        }
                        catch (Exception)
                        {
                            throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
                        }
                    }
                    // list for adding the different rides
                    List<Ride> list = new List<Ride>();
                    list.AddRange(rides);
                    this.userRides.Add(userId, list);
                }
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides are null");
            }
        }
        /// <summary>
        /// Returns the rides as array of  the particulat userid passed else throws exception
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="CabInvoiceGenerator.CabInvoiceException">Invalid user ID</exception>
        public Ride[] GetRides(string userId)
        {
            try
            {
                return this.userRides[userId].ToArray();
            }
            catch (Exception)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid user ID");
            }
        }
    }
}
