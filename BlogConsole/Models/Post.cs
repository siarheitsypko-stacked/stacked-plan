using System;

namespace BlogConsole.Models
{
    /// <summary>
    /// Представляет пост в блоге.
    /// </summary>
    public class Post
    {
        private static int _nextId = 1; // статический счётчик для уникальных ID

        /// <summary>
        /// Уникальный идентификатор поста.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Заголовок поста.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержание поста.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Дата и время создания поста.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Конструктор, автоматически присваивает следующий ID.
        /// </summary>
        public Post()
        {
            Id = _nextId++;
        }

        /// <summary>
        /// Сброс счётчика ID (используется при загрузке из файла).
        /// </summary>
        /// <param name="newId">Новое значение для счётчика (обычно максимальный ID + 1).</param>
        public static void ResetNextId(int newId)
        {
            _nextId = newId;
        }
    }
}