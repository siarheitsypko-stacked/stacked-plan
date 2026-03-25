# Stacked — портфолио и система обучения C# / .NET

Этот репозиторий содержит полную экосистему моего обучения и разработки: от документации и плана до исходного кода проектов. Здесь вы найдёте всё, что связано с моим путём к профессии C# / .NET разработчика.

---

## 📁 Структура репозитория

- **[`docs/`](./docs)** — публичная документация проекта (сайт на GitHub Pages).  
  Включает:
  - Главный роадмап (`roadmap.html`)
  - Профиль разработчика (`profile.html`)
  - Страницу проектов (`projects.html`)
  - Резюме (`resume.html`)
  - Детальный план на 112 дней (`detailed-plan.html`)
  - Заметки и ресурсы (`notes.html`)
  - Техническое задание (`specification.html`)
  - Практику (`practise.html`)
  - Коллекцию промптов (`prompts.html`)

- **[`src/`](./src)** — исходный код основного проекта **Stacked API** (Clean Architecture, CQRS, ASP.NET Core).  
  Внутри:
  - `Stacked/` — решение, сгенерированное из шаблона `Clean.Architecture.Solution.Template`.
  - `BlogConsole/` — консольный прототип блога (CRUD, сохранение в JSON).

- **[`playground/`](./playground)** — песочница для учебных проектов:
  - `csharp-in-hour/` — проект «C# за час» (минимальное консольное приложение)
  - `typescript-in-hour/` — проект «TypeScript за час» (простые скрипты)
  - В будущем здесь появятся другие экспериментальные проекты.

---

## 🚀 Сайт документации

Документация опубликована на GitHub Pages и доступна по адресу:  
[https://siarheitsypko-stacked.github.io/stacked-plan/](https://siarheitsypko-stacked.github.io/stacked-plan/)

Там вы найдёте подробный план обучения, техническое задание, профиль разработчика и многое другое.

---

## 🧪 Как запустить проекты

### 1. Stacked API (основной проект)

```bash
cd src/Stacked
dotnet build
cd src/AppHost
dotnet run
После запуска откроется .NET Aspire dashboard, а также Swagger UI для тестирования API.

2. BlogConsole
bash
cd src/BlogConsole
dotnet run
3. C# / TypeScript hour
Проекты в playground/ содержат только README.md – их можно дополнять по мере изучения.

📚 Используемые технологии
C# / .NET 8/9 – основной язык и платформа

ASP.NET Core Web API – бэкенд

Entity Framework Core – ORM

Clean Architecture + CQRS – архитектурный подход

React + TypeScript – фронтенд (в планах)

Docker, Azure, GitHub Actions – CI/CD и деплой

Telegram Bot API, Ollama / DeepSeek-R1 – AI-интеграции

🧭 О проекте
Stacked — это не просто код. Это система обучения, где каждый этап документирован, структурирован и привязан к реальным задачам. Проект растёт вместе со мной и отражает мой путь к профессии.

Цель — стать востребованным C# / .NET разработчиком, готовым к работе на себя (фриланс, международные контракты) или релокации в IT-компанию.

📫 Контакты
GitHub: siarheitsypko-stacked

Email: snowleopardstb@gmail.com

Telegram: @Luter (по запросу)

📄 Лицензия
Весь код и документация распространяются под лицензией MIT. Вы можете свободно использовать материалы для обучения или адаптировать их для своих проектов.

Этот README будет обновляться по мере развития проекта. Следите за обновлениями!