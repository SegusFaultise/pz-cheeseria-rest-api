namespace PZCheeseriaRestApi.Exceptions
{
    public class GetCheeseProductByIdException : Exception
    {
        public GetCheeseProductByIdException() {}

        public GetCheeseProductByIdException(string message): base(message) {}

        public GetCheeseProductByIdException(string message, Exception inner_exception) : base(message, inner_exception) {}
    }
}
