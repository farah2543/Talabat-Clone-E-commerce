namespace Domain.Exceptions.NotFoundExceptions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

    }
}
