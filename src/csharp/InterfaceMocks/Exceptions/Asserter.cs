using System;

namespace InterfaceMocks.Exceptions
{
    /// <inheritdoc/>
    internal sealed class Asserter : IAsserter
    {
        /// <inheritdoc/>
        public void AssertIf(bool condition, string exceptionMsg)
        {
            if (!condition) return;
            throw new Exception(exceptionMsg);
        }
    }

    /// <summary>
    /// Provides Assert capability without relying on any specific framework.
    /// </summary>
    internal interface IAsserter
    {
        /// <summary>
        /// Thows an <see cref="Exception"/> with <see cref="exceptionMsg"/> as the message if <see cref="condition"/> is true.
        /// </summary>
        /// <param name="condition">Will throw an <see cref="Exception"/> when this is true.</param>
        /// <param name="exceptionMsg">The message of the <see cref="Exception"/></param>
        void AssertIf(bool condition, string exceptionMsg);
    }
}