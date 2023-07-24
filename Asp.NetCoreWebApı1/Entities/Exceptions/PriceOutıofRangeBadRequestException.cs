namespace Entities.Exceptions
{


    public class PriceOutıofRangeBadRequestException : BadRequestException
    {
        public PriceOutıofRangeBadRequestException() : base("Max price should be less than 1000 and grater than 10")
        {
        }
    }
}
