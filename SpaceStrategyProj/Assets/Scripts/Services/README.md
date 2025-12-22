# Backend Service для Unity

Сервис для проверки состояния бэкенда и вывода информации в консоль Unity.

## Использование

### Способ 1: Автоматическая проверка

1. Добавьте компонент `BackendChecker` на любой GameObject в сцене
2. Настройте параметры в Inspector:
   - **Backend Url**: URL бэкенда (по умолчанию `http://localhost:8080`)
   - **Check Interval**: Интервал проверки в секундах (по умолчанию 10 секунд)
   - **Check On Start**: Проверить сразу при старте

3. Запустите сцену - сервис автоматически начнет проверять состояние бэкенда

### Способ 2: Использование BackendService напрямую

```csharp
using SpaceStrategy.Services;

public class MyScript : MonoBehaviour
{
    private BackendService backendService;

    void Start()
    {
        backendService = gameObject.AddComponent<BackendService>();
        backendService.SetBackendUrl("http://localhost:8080");
        backendService.SetCheckInterval(5f);
        backendService.StartChecking();
    }

    // Ручная проверка
    void OnButtonClick()
    {
        backendService.CheckBackendStatus();
    }
}
```

## Что выводится в консоль

### При успешном подключении:
```
=== ИНФОРМАЦИЯ О БЭКЕНДЕ ===
Название сервиса: SpaceBackend
Версия: 1.0.0
Время запуска: 2024-01-15T10:30:00Z
--- Информация о базе данных ---
Подключено: ДА
Имя БД: spacedb
Версия сервера: PostgreSQL 16.x on ...
===========================
```

### При ошибке подключения:
```
[BackendService] БЭКЕНД НЕДОСТУПЕН!
[BackendService] Ошибка подключения: не удалось установить соединение с http://localhost:8080
[BackendService] Проверьте, что бэкенд запущен и доступен по адресу: http://localhost:8080
```

## Настройка URL бэкенда

Если бэкенд запущен не на localhost:8080, измените URL в Inspector компонента `BackendChecker` или программно:

```csharp
backendService.SetBackendUrl("http://your-backend-url:port");
```

## Структура файлов

- `BackendService.cs` - основной сервис для работы с API
- `BackendChecker.cs` - простой компонент для быстрого старта
- `Models/ServiceInfo.cs` - модели данных для десериализации JSON
