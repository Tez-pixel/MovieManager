using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager;

// Contains the business rules for managing the movie catalogue.
public class MovieService
{
    private readonly MovieRepository repository;
    private readonly List<Movie> movies;

    // Loads the saved movies when the service is created.
    public MovieService(MovieRepository repository)
    {
        this.repository = repository;
        movies = repository.LoadMovies();
    }

    // Adds a movie after checking the input is valid.
    public void AddMovie(string title, string genre, int rating)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");
        if (string.IsNullOrWhiteSpace(genre))
            throw new ArgumentException("Genre cannot be empty.");
        if (rating < 1 || rating > 10)
            throw new ArgumentException("Rating must be between 1 and 10.");

        movies.Add(new Movie(title.Trim(), genre.Trim(), rating));
        repository.SaveMovies(movies);
    }

    // Returns a copy of all movies.
    public List<Movie> GetAllMovies()
    {
        return new List<Movie>(movies);
    }

    // Finds movies whose title contains the keyword (ignores capital letters).
    public List<Movie> SearchByTitle(string keyword)
    {
        return movies
            .Where(m => m.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Marks a movie as watched. Returns false if no movie has that title.
    public bool MarkAsWatched(string title)
    {
        Movie? movie = movies.FirstOrDefault(m =>
            m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (movie == null)
        {
            return false;
        }

        movie.MarkAsWatched();
        repository.SaveMovies(movies);
        return true;
    }

    // Returns only the watched movies.
    public List<Movie> GetWatched()
    {
        return movies.Where(m => m.IsWatched).ToList();
    }

    // Returns only the movies not yet watched.
    public List<Movie> GetUnwatched()
    {
        return movies.Where(m => !m.IsWatched).ToList();
    }
}
