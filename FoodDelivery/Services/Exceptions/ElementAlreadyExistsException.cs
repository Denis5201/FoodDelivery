namespace FoodDelivery.Services.Exceptions
{
    [Serializable]
    public class ElementAlreadyExistsException : Exception
    {
        public ElementAlreadyExistsException() { }

        public ElementAlreadyExistsException(string message) : base(message) { }

        public ElementAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
