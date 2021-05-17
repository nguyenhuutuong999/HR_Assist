namespace HR.Assist.Core.Infrastructure.Exceptions
{
    using System;

    /// <summary>
    ///    Exception type for app exceptions.
    /// </summary>
    /// <seealso cref="System.Exception"/>
    public class HRAssistDomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HRAssistDomainException"/> class.
        /// </summary>
        public HRAssistDomainException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HRAssistDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public HRAssistDomainException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HRAssistDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public HRAssistDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
