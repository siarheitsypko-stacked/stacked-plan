using System;
using System.IO;
using System.Text.Json;

class Program
{
    static List<Post> posts = new List<Post>();
    const string fileName = "posts.json";
    // Точка входа в программу
    static void Main()
    {
        bool isRunning = true;
        LoadPostsFromFile();

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Меню блога ===\n");
            Console.WriteLine("1. Создать новый пост");
            Console.WriteLine("2. Показать все посты");
            Console.WriteLine("3. Выйти");
            Console.Write("\nВыберите пункт: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreatePost();
                    break;
                case "2":
                    ShowAllPosts();
                    break;
                case "3":
                    isRunning = false;
                    SavePostsToFile();
                    Console.WriteLine("Выход из программы...");
                    break;
                default:
                    Console.WriteLine("Неверный пункт. Нажмите любую клавишу...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Метод для создания нового поста
    // Выносим код в отдельный метод, чтобы не загромождать Main()
    
        static void CreatePost()
        {
            Console.Clear();
            Console.WriteLine("=== Создание нового поста ===\n");

            Console.Write("Введите название поста: ");
            string title = Console.ReadLine();

            if (title.Length < 3)
            {
                Console.WriteLine("Ошибка: название должно быть не короче 3 символов.");
            }
            else
            {
                Console.Write("Введите содержание поста: ");
                string content = Console.ReadLine();

                if (content.Length == 0)
                {
                    Console.WriteLine("Ошибка: содержание не может быть пустым.");
                }
                else
                {
                    posts.Add(new Post { Title = title, Content = content, CreatedAt = DateTime.Now });
                    Console.WriteLine($"\nПост '{title}' добавлен.");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
        static void ShowAllPosts()
    {
        Console.Clear();
        Console.WriteLine("=== Все посты ===\n");

        if (posts.Count == 0)
        {
            Console.WriteLine("Постов пока нет.");
        }
        else
        {
            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.Id}");
                Console.WriteLine($"Название: {post.Title}");
                Console.WriteLine($"Содержание: {post.Content}");
                Console.WriteLine($"Создан: {post.CreatedAt}");
                Console.WriteLine("----------");
            }
        }

        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }
    static void SavePostsToFile()
    {
        string json = JsonSerializer.Serialize(posts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    static void LoadPostsFromFile()
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            posts = JsonSerializer.Deserialize<List<Post>>(json) ?? new List<Post>();
        }
    }
}
class Post
{
    private static int nextId = 1; // статическое поле для генерации уникальных ID
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public Post()
    {
        Id = nextId++;
    }
}
