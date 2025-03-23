namespace Hotels.Service.Exceptions
{
    public class GuestAlreadyExistsException:Exception
    {
        public GuestAlreadyExistsException(string message):base(message)
        {
            
        }
    }
}
