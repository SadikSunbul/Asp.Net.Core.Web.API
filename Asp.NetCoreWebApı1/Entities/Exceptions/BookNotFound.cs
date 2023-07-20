namespace Entities.Exceptions
{
    public sealed class BookNotFound : NotFountExeption //kalıtım alınamaz
    {
        public BookNotFound(int id) : base($"The book with id:{id} couldnot found")
        {
        }
    }
}
