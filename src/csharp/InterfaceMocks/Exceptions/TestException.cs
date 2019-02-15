using System;

namespace InterfaceMocks.Exceptions
{
    /// <summary>
    /// <see cref="T:System.Exception" /> thrown when test configuration is incorrect.
    /// </summary>
    public sealed class TestException : Exception
    {
        private readonly string _method;

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"/> class.
        /// </summary>
        /// <param name="method"></param>
        public TestException(string method) => _method = method;


        /// <inheritdoc />
        public override string Message => $"If you want to use {_method}, configure via Builder.";
    }
}