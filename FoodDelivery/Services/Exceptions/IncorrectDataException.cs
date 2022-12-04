namespace FoodDelivery.Services.Exceptions
{
    [Serializable]
    public class IncorrectDataException : Exception
    {
        public IncorrectDataException() { }

        public IncorrectDataException(string message) : base(message) { }

        public IncorrectDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
