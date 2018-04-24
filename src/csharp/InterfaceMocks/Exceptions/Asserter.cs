using System;

namespace InterfaceMocks.Exceptions
{
    /// <summary>
    /// Provides Assert capability without relying on any specific framework.
    /// </summary>
    internal sealed class Asserter : IAsserter
    {
        /// <summary>
        /// Thows an <see cref="Exception"/> with <see cref="exceptionMsg"/> as the message if <see cref="condition"/> is not true.
        /// </summary>
        /// <param name="condition">Will throw an <see cref="Exception"/> when this is false.</param>
        /// <param name="exceptionMsg">The message of the <see cref="Exception"/></param>
        public void AssertIf(bool condition, string exceptionMsg)
        {
            if (condition) return;
            throw new Exception(exceptionMsg);
        }
    }

    internal interface IAsserter
    {
        void AssertIf(bool condition, string exceptionMsg);
    }
}