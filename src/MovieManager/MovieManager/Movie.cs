using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieManager;

// Represents one movie in the catalogue.
public class Movie
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Rating { get; set; }
    public bool IsWatched { get; set; }

    // Creates a new movie. A new movie starts as not watched.
    public Movie(string title, string genre, int rating)
    {
        Title = title;
        Genre = genre;
        Rating = rating;
        IsWatched = false;
    }

    // Marks this movie as watched.
    public void MarkAsWatched()
    {
        IsWatched = true;
    }

    // Controls how the movie is shown when printed.
    public override string ToString()
    {
        string status = IsWatched ? "Watched" : "Not watched";
        return $"{Title} | {Genre} | Rating: {Rating}/10 | {status}";
    }
}
