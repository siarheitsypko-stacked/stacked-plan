using System;

namespace BlogConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                    int contentLength = content.Length;
                    Console.WriteLine($"\nПост '{title}' готов к публикации.");
                    Console.WriteLine($"Длина содержания: {contentLength} символов.");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}