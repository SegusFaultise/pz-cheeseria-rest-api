namespace PZCheeseriaRestApi.Exceptions
{
    /// <summary>
    /// Exception thrown when there is an error retrieving a cheese product by its name.
    /// </summary>
    [Serializable]
    internal class GetCheeseProductByNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCheeseProductByNameException"/> class.
        /// </summary>
        public GetCheeseProductByNameException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCheeseProductByNameException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GetCheeseProductByNameException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCheeseProductByNameException"/> class
        /// with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public GetCheeseProductByNameException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
