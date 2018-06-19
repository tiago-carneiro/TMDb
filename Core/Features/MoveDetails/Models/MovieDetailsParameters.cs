namespace TMDb.Core
{
    public class MovieDetailsParameters
    {
        public int Id { get; }

        public MovieDetailsParameters(int id)
            => Id = id;
    }
}
