using System;

namespace TMDb.Core
{
    public abstract class BaseMovieWrapper
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PosterPath { get; set; }

        public string BackdropPath { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Genres { get; set; }
    }
}
