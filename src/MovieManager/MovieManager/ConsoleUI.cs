using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager;

// Handles all input and output with the user. Contains no business rules.
public class ConsoleUI
{
    private readonly MovieService service;

    public ConsoleUI(MovieService service)
    {
        this.service = service;
    }

    // Shows the menu again and again until the user chooses Exit.
    public void Run()
    {
        bool running = true;
        while (running)
        {
            ShowMenu();
            string choice = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1": AddMovie(); break;
                case "2": ShowList("All movies", service.GetAllMovies()); break;
                case "3": SearchMovies(); break;
                case "4": MarkWatched(); break;
                case "5": ShowList("Watched movies", service.GetWatched()); break;
                case "6": ShowList("Unwatched movies", service.GetUnwatched()); break;
                case "0": running = false; Console.WriteLine("Goodbye!"); break;
                default: Console.WriteLine("Invalid choice. Please type a number from the menu."); break;
            }
            Console.WriteLine();
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("=== Movie Collection Manager ===");
        Console.WriteLine("1. Add movie");
        Console.WriteLine("2. View all movies");
        Console.WriteLine("3. Search by title");
        Console.WriteLine("4. Mark as watched");
        Console.WriteLine("5. View watched movies");
        Console.WriteLine("6. View unwatched movies");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    private void AddMovie()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Genre: ");
        string genre = Console.ReadLine() ?? "";
        int rating = ReadRating();

        try
        {
            service.AddMovie(title, genre, rating);
            Console.WriteLine("Movie added successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Keeps asking until the user types a whole number.
    private int ReadRating()
    {
        while (true)
        {
            Console.Write("Rating (1-10): ");
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out int rating))
                return rating;
            Console.WriteLine("Error: the rating must be a whole number.");
        }
    }

    private void SearchMovies()
    {
        Console.Write("Enter part of the title: ");
        string text = Console.ReadLine() ?? "";
        var results = service.SearchByTitle(text);
        if (results.Count == 0)
            Console.WriteLine("No movie found.");
        else
            ShowList("Search results", results);
    }

    private void MarkWatched()
    {
        Console.Write("Enter the exact title: ");
        string title = Console.ReadLine() ?? "";
        if (service.MarkAsWatched(title))
            Console.WriteLine("Marked as watched.");
        else
            Console.WriteLine("No movie with that title was found.");
    }

    private void ShowList(string heading, List<Movie> movies)
    {
        Console.WriteLine("--- " + heading + " ---");
        if (movies.Count == 0)
        {
            Console.WriteLine("There are no movies to show.");
            return;
        }
        foreach (Movie m in movies)
            Console.WriteLine(m);
    }
}

