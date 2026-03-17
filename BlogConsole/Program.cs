using System;
using System.Linq;
using BlogConsole.Models;
using BlogConsole.Repositories;

namespace BlogConsole
{
    class Program
    {
        private static readonly IPostRepository _repository = new PostRepository();

        static void Main(string[] args)
        {
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

                if (string.IsNullOrWhiteSpace(content))
                {
                    Console.WriteLine("Ошибка: содержание не может быть пустым.");
                }
                else
                {
                    var post = new Post { Title = title, Content = content, CreatedAt = DateTime.Now };
                    _repository.Add(post);
                    Console.WriteLine($"\nПост '{title}' добавлен.");
                }
            }

            Pause();
        }

        static void ShowAllPosts()
        {
            Console.Clear();
            Console.WriteLine("=== Все посты ===\n");

            var allPosts = _repository.GetAll();
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

            Pause();
        }

        static void EditPost()
        {
            Console.Clear();
            Console.WriteLine("=== Редактирование поста ===\n");
            ShowAllPosts();

            var allPosts = _repository.GetAll();
            if (allPosts.Count == 0) return;

            Console.Write("Введите ID поста для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var post = _repository.GetById(id);
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

                    _repository.Update(post);
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

            Pause();
        }

        static void DeletePost()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление поста ===\n");
            ShowAllPosts();

            var allPosts = _repository.GetAll();
            if (allPosts.Count == 0) return;

            Console.Write("Введите ID поста для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var post = _repository.GetById(id);
                if (post != null)
                {
                    Console.Write($"Удалить пост '{post.Title}'? (y/n): ");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        _repository.Delete(id);
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

            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}