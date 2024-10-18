namespace PZCheeseriaRestApi.Exceptions
{
    public class UpdateCheeseProductException : Exception
    {
        public UpdateCheeseProductException() {}

        public UpdateCheeseProductException(string message) : base(message) {}

        public UpdateCheeseProductException(string message, Exception inner_exception) : base(message, inner_exception) {}
    }
}
