using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    /// <summary>
    /// Exception class for handling different exceptions
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CabInvoiceException : Exception
    {
        //Enum constants for different type of exceptions
        public enum ExceptionType
        {
            INVALID_RIDE_TYPE,
            INVALID_DISTANCE,
            INVALID_TIME,
            NULL_RIDES,
            INVALID_USER_ID
        }
        ExceptionType type;
        /// <summary>
        /// Constructor for overriding the message with the exception type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        public CabInvoiceException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
