using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;

namespace MovieManager;

// Saves movies to a JSON file and loads them back.
public class MovieRepository
{
    private readonly string filePath;

    public MovieRepository(string filePath)
    {
        this.filePath = filePath;
    }

    // Writes the whole list of movies to the file.
    public void SaveMovies(List<Movie> movies)
    {
        string json = JsonSerializer.Serialize(movies,
            new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    // Reads the movies from the file. Returns an empty list if the file
    // is missing or damaged, so the app never crashes on startup.
    public List<Movie> LoadMovies()
    {
        if (!File.Exists(filePath))
        {
            return new List<Movie>();
        }

        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Movie>>(json) ?? new List<Movie>();
        }
        catch (JsonException)
        {
            return new List<Movie>();
        }
    }
}
