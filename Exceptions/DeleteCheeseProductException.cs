namespace PZCheeseriaRestApi.Exceptions
{
    public class DeleteCheeseProductException : Exception
    {
        public DeleteCheeseProductException() {}

        public DeleteCheeseProductException(string message) : base(message) {}

        public DeleteCheeseProductException(string message, Exception inner_exception) : base(message, inner_exception) {}
    }
}
