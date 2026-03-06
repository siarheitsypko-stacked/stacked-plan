using System;

class Program
{
    // Точка входа в программу
    static void Main()
    {
        // Флаг, управляющий работой главного цикла программы
        // Пока isRunning == true, меню будет показываться снова и снова
        bool isRunning = true;

        // Главный цикл программы (while)
        // Он будет выполняться до тех пор, пока isRunning не станет false
        while (isRunning)
        {
            // Очищаем экран консоли, чтобы меню выглядело аккуратно
            Console.Clear();

            // Выводим пункты меню
            Console.WriteLine("=== Меню блога ===\n");
            Console.WriteLine("1. Создать новый пост");
            Console.WriteLine("2. Выйти");
            Console.Write("\nВыберите пункт: ");

            // Считываем выбор пользователя (это будет строка: "1" или "2")
            string choice = Console.ReadLine();

            // Оператор switch проверяет значение переменной choice
            // и выполняет соответствующий блок кода
            switch (choice)
            {
                // Если пользователь ввёл "1" — вызываем метод CreatePost()
                case "1":
                    CreatePost();  // Вызов метода для создания поста
                    break;         // break обязателен, чтобы выйти из switch

                // Если пользователь ввёл "2" — завершаем программу
                case "2":
                    isRunning = false;  // Меняем флаг, чтобы выйти из while
                    Console.WriteLine("Выход из программы...");
                    break;

                // Если пользователь ввёл что-то другое — сообщаем об ошибке
                default:
                    Console.WriteLine("Неверный пункт. Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();  // Ждём нажатия любой клавиши
                    break;
            }
        }
    }

    // Метод для создания нового поста
    // Выносим код в отдельный метод, чтобы не загромождать Main()
    static void CreatePost()
    {
        // Очищаем экран перед созданием поста
        Console.Clear();
        Console.WriteLine("=== Создание нового поста ===\n");

        // Ввод названия поста
        Console.Write("Введите название поста: ");
        string title = Console.ReadLine();

        // Проверяем длину названия
        if (title.Length < 3)
        {
            Console.WriteLine("Ошибка: название должно быть не короче 3 символов.");
        }
        else
        {
            // Ввод содержания поста
            Console.Write("Введите содержание поста: ");
            string content = Console.ReadLine();

            // Проверяем, что содержание не пустое
            if (content.Length == 0)
            {
                Console.WriteLine("Ошибка: содержание не может быть пустым.");
            }
            else
            {
                // Вычисляем длину содержания
                int contentLength = content.Length;

                // Выводим результат
                Console.WriteLine($"\nПост '{title}' готов к публикации.");
                Console.WriteLine($"Длина содержания: {contentLength} символов.");
            }
        }

        // Пауза, чтобы пользователь увидел результат, а затем вернуться в меню
        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
        Console.ReadKey();
    }
}