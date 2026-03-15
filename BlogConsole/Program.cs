using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// ==================== Интерфейс репозитория ====================
// Задаёт контракт для работы с хранилищем постов.
public interface IPostRepository
{
    List<Post> GetAll();           // получить все посты
    Post GetById(int id);          // получить пост по ID
    void Add(Post post);           // добавить новый пост
    void Update(Post post);        // обновить пост (сохранить изменения)
    void Delete(int id);           // удалить пост по ID
    void SaveChanges();            // явно сохранить изменения (если нужно)
    void Load();                   // загрузить данные из хранилища
}

// ==================== Репозиторий, работающий с JSON ====================
public class PostRepository : IPostRepository
{
    private List<Post> posts = new List<Post>();   // внутренний список
    private const string fileName = "posts.json";   // файл для хранения

    // При создании репозитория автоматически загружаем данные
    public PostRepository()
    {
        Load();
    }

    // Возвращает все посты
    public List<Post> GetAll() => posts;

    // Ищет пост по ID с помощью LINQ
    public Post GetById(int id) => posts.FirstOrDefault(p => p.Id == id);

    // Добавляет пост и сразу сохраняет изменения
    public void Add(Post post)
    {
        posts.Add(post);
        SaveChanges();
    }

    // Обновление – просто сохраняем (объект уже мог быть изменён)
    public void Update(Post post) => SaveChanges();

    // Удаляет пост, если найден, и сохраняет
    public void Delete(int id)
    {
        var post = GetById(id);
        if (post != null)
        {
            posts.Remove(post);
            SaveChanges();
        }
    }

    // Сериализует список в JSON и записывает в файл
    public void SaveChanges()
    {
        string json = JsonSerializer.Serialize(posts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    // Читает файл и десериализует обратно в список
    public void Load()
    {
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            posts = JsonSerializer.Deserialize<List<Post>>(json) ?? new List<Post>();
        }
    }
}

// ==================== Класс, представляющий один пост ====================
public class Post
{
    private static int nextId = 1;   // статический счётчик для уникальных ID
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    // Конструктор автоматически присваивает следующий ID
    public Post()
    {
        Id = nextId++;
    }
}

// ==================== Основная программа ====================
class Program
{
    // Вместо прямого списка используем репозиторий через интерфейс
    static IPostRepository repository = new PostRepository();

    static void Main()
    {
        // Загрузка уже выполняется в конструкторе репозитория, поэтому отдельно вызывать не нужно

        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Меню блога ===\n");
            Console.WriteLine("1. Создать новый пост");
            Console.WriteLine("2. Показать все посты");
            Console.WriteLine("3. Выйти");
            Console.WriteLine("4. Редактировать пост");
            Console.WriteLine("5. Удалить пост");
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
                    Console.WriteLine("Выход из программы...");
                    break;
                case "4":
                    EditPost();
                    break;
                case "5":
                    DeletePost();
                    break;
                default:
                    Console.WriteLine("Неверный пункт. Нажмите любую клавишу...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // ========== Метод создания нового поста ==========
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
                // Добавляем через репозиторий
                repository.Add(new Post { Title = title, Content = content, CreatedAt = DateTime.Now });
                Console.WriteLine($"\nПост '{title}' добавлен.");
            }
        }

        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    // ========== Метод отображения всех постов ==========
    static void ShowAllPosts()
    {
        Console.Clear();
        Console.WriteLine("=== Все посты ===\n");

        var allPosts = repository.GetAll();
        if (allPosts.Count == 0)
        {
            Console.WriteLine("Постов пока нет.");
        }
        else
        {
            foreach (var post in allPosts)
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

    // ========== Метод редактирования поста ==========
    static void EditPost()
    {
        Console.Clear();
        Console.WriteLine("=== Редактирование поста ===\n");
        ShowAllPosts();   // показываем список, чтобы пользователь видел ID

        if (repository.GetAll().Count == 0)
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
            return;
        }

        Console.Write("Введите ID поста для редактирования: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var post = repository.GetById(id);
            if (post != null)
            {
                Console.WriteLine($"Редактируем пост: {post.Title}");
                Console.Write("Новый заголовок (оставьте пустым, чтобы не менять): ");
                string newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle) && newTitle.Length >= 3)
                    post.Title = newTitle;
                else if (!string.IsNullOrWhiteSpace(newTitle))
                    Console.WriteLine("Название слишком короткое, оставлено старое.");

                Console.Write("Новое содержание (оставьте пустым, чтобы не менять): ");
                string newContent = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newContent))
                    post.Content = newContent;

                repository.Update(post);   // сохраняем изменения
                Console.WriteLine("Пост обновлён.");
            }
            else
            {
                Console.WriteLine("Пост с таким ID не найден.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ID.");
        }

        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    // ========== Метод удаления поста ==========
    static void DeletePost()
    {
        Console.Clear();
        Console.WriteLine("=== Удаление поста ===\n");
        ShowAllPosts();

        if (repository.GetAll().Count == 0)
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
            return;
        }

        Console.Write("Введите ID поста для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var post = repository.GetById(id);
            if (post != null)
            {
                Console.Write($"Удалить пост '{post.Title}'? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    repository.Delete(id);
                    Console.WriteLine("Пост удалён.");
                }
                else
                {
                    Console.WriteLine("Удаление отменено.");
                }
            }
            else
            {
                Console.WriteLine("Пост с таким ID не найден.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ID.");
        }

        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }
}