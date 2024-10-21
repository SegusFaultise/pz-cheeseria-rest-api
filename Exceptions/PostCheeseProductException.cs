namespace PZCheeseriaRestApi.Exceptions
{
    /// <summary>
    /// Exception thrown when there is an error posting a new cheese product.
    /// </summary>
    public class PostCheeseProductException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostCheeseProductException"/> class.
        /// </summary>
        public PostCheeseProductException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCheeseProductException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PostCheeseProductException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCheeseProductException"/> class
        /// with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner_exception">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PostCheeseProductException(string message, Exception inner_exception)
            : base(message, inner_exception) { }
    }
}

