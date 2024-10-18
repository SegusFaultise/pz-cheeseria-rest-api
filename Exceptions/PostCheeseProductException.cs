namespace PZCheeseriaRestApi.Exceptions
{
    public class PostCheeseProductException : Exception
    {
        public PostCheeseProductException() {}

        public PostCheeseProductException(string message) : base(message) {}

        public PostCheeseProductException(string message, Exception inner_exception) : base(message, inner_exception) {}
    }
}
