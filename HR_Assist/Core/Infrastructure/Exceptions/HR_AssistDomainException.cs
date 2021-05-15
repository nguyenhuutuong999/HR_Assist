namespace HR_Assist.Core.Infrastructure.Exceptions
{
    using System;

    /// <summary>
    ///    Exception type for app exceptions.
    /// </summary>
    /// <seealso cref="System.Exception"/>
    public class HR_AssistDomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HR_AssistDomainException"/> class.
        /// </summary>
        public HR_AssistDomainException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HR_AssistDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public HR_AssistDomainException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HR_AssistDomainException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public HR_AssistDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
