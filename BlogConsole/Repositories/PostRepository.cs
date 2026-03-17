using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BlogConsole.Models;

namespace BlogConsole.Repositories
{
    /// <summary>
    /// Реализация репозитория, хранящая посты в JSON-файле.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private readonly List<Post> _posts = new();
        private readonly string _filePath;

        /// <summary>
        /// Конструктор. При создании автоматически загружает данные из файла.
        /// </summary>
        /// <param name="filePath">Путь к JSON-файлу (по умолчанию "posts.json")</param>
        public PostRepository(string filePath = "posts.json")
        {
            _filePath = filePath;
            Load();
        }

        public List<Post> GetAll() => _posts;

        public Post GetById(int id) => _posts.FirstOrDefault(p => p.Id == id);

        public void Add(Post post)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            _posts.Add(post);
            SaveChanges();
        }

        public void Update(Post post)
        {
            // обновление уже произошло в самом объекте, просто сохраняем
            SaveChanges();
        }

        public void Delete(int id)
        {
            var post = GetById(id);
            if (post != null)
            {
                _posts.Remove(post);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_posts, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                // логируем ошибку, но не даём программе упасть
                Console.WriteLine($"Ошибка сохранения данных: {ex.Message}");
            }
        }

        public void Load()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    var loaded = JsonSerializer.Deserialize<List<Post>>(json);
                    if (loaded != null)
                    {
                        _posts.Clear();
                        _posts.AddRange(loaded);
                        // восстановим статический счётчик, чтобы ID продолжались
                        if (_posts.Any())
                            Models.Post.ResetNextId(_posts.Max(p => p.Id) + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки данных: {ex.Message}");
            }
        }
    }
}