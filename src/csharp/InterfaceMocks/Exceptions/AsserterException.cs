using System;

namespace InterfaceMocks.Exceptions
{
    /// <summary>
    /// <see cref="T:System.Exception" /> thrown when validation is not as expected.
    /// </summary>
    public sealed class AsserterException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="T:ValidationException"></see> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        /// <summary></summary>
        public AsserterException(string message):base(message){}
    }
}