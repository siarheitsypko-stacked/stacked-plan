using System.Collections.Generic;
using BlogConsole.Models;

namespace BlogConsole.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с постами.
    /// Позволяет легко заменить реализацию (файл, база данных, API и т.д.).
    /// </summary>
    public interface IPostRepository
    {
        List<Post> GetAll();
        Post GetById(int id);
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
        void SaveChanges(); // сохранить текущее состояние (если используется)
        void Load();        // загрузить данные
    }
}