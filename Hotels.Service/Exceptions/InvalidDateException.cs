namespace Hotels.Service.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException() : base($"Invalid date has been passed")
        {
        }
    }
}
