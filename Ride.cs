using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Ride class for different rides
    /// </summary>
    public class Ride
    {
        public double distance;
        public int time;
        /// <summary>
        /// Construtor for ride attributes
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="time">The time.</param>
        public Ride(double distance, int time)
        {
            this.distance = distance;
            this.time = time;
        }
    }
}
