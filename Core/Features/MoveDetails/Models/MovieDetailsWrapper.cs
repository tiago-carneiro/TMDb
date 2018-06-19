namespace TMDb.Core
{
    public class MovieDetailsWrapper : BaseMovieWrapper
    {
        public string Overview { get; set; }

        public string Tagline { get; set; }

        public decimal VoteAverage { get; set; }
    }
}
