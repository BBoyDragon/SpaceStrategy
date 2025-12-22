# Документация по проверке API SpaceBackend

## Описание

SpaceBackend - это ASP.NET приложение, реализованное в стиле трехслойной архитектуры:
- **Controllers** - слой представления (API контроллеры)
- **Services** - слой бизнес-логики
- **Repositories** - слой доступа к данным

## Требования

- Docker и Docker Compose
- .NET 8.0 SDK (для локальной разработки, опционально)

## Запуск приложения

### Запуск через Docker Compose

1. Перейдите в директорию `SpaceBackend`:
```bash
cd SpaceBackend
```

2. Запустите приложение и базу данных:
```bash
docker-compose up --build
```

Приложение будет доступно по адресу: **http://localhost:8080**

### Остановка приложения

```bash
docker-compose down
```

Для удаления volumes (данные БД будут удалены):
```bash
docker-compose down -v
```

## Доступные эндпоинты

### 1. Получение информации о сервисе и базе данных

**Метод:** `GET`  
**URL:** `/api/ServiceInfo`  
**Полный путь:** `http://localhost:8080/api/ServiceInfo`

**Описание:**  
Возвращает информацию о сервисе (название, версия, время запуска) и состоянии подключения к базе данных PostgreSQL.

**Ответ (200 OK):**
```json
{
  "serviceName": "SpaceBackend",
  "version": "1.0.0",
  "startTime": "2024-01-15T10:30:00Z",
  "databaseInfo": {
    "isConnected": true,
    "databaseName": "spacedb",
    "serverVersion": "PostgreSQL 16.x on ...",
    "errorMessage": null
  }
}
```

**Ответ при ошибке подключения к БД:**
```json
{
  "serviceName": "SpaceBackend",
  "version": "1.0.0",
  "startTime": "2024-01-15T10:30:00Z",
  "databaseInfo": {
    "isConnected": false,
    "databaseName": null,
    "serverVersion": null,
    "errorMessage": "Connection refused"
  }
}
```

## Тестирование API

### Способ 1: Использование Swagger UI (Рекомендуется)

1. Откройте браузер и перейдите по адресу:
   ```
   http://localhost:8080/swagger
   ```

2. В Swagger UI вы увидите список всех доступных эндпоинтов.

3. Для тестирования эндпоинта `/api/ServiceInfo`:
   - Найдите раздел `ServiceInfo`
   - Нажмите на `GET /api/ServiceInfo`
   - Нажмите кнопку **"Try it out"**
   - Нажмите кнопку **"Execute"**
   - Просмотрите ответ в разделе **"Responses"**

### Способ 2: Использование curl

```bash
curl -X GET http://localhost:8080/api/ServiceInfo
```

С форматированием JSON:
```bash
curl -X GET http://localhost:8080/api/ServiceInfo | json_pp
```

### Способ 3: Использование PowerShell (Windows)

```powershell
Invoke-RestMethod -Uri http://localhost:8080/api/ServiceInfo -Method Get
```

С форматированием:
```powershell
Invoke-RestMethod -Uri http://localhost:8080/api/ServiceInfo -Method Get | ConvertTo-Json
```

### Способ 4: Использование Postman

1. Создайте новый запрос
2. Установите метод: `GET`
3. Введите URL: `http://localhost:8080/api/ServiceInfo`
4. Нажмите **Send**
5. Просмотрите ответ

### Способ 5: Использование браузера

Просто откройте в браузере:
```
http://localhost:8080/api/ServiceInfo
```

Браузер автоматически выполнит GET запрос и покажет JSON ответ.

## Проверка работоспособности

### 1. Проверка доступности приложения

```bash
curl http://localhost:8080/swagger
```

Должна открыться страница Swagger UI.

### 2. Проверка подключения к базе данных

Выполните запрос к `/api/ServiceInfo` и проверьте поле `databaseInfo.isConnected`:
- `true` - подключение успешно
- `false` - есть проблемы с подключением (проверьте логи контейнера)

### 3. Проверка логов

Просмотр логов backend контейнера:
```bash
docker logs space_backend
```

Просмотр логов PostgreSQL:
```bash
docker logs space_postgres
```

Просмотр логов всех сервисов:
```bash
docker-compose logs
```

## Структура проекта

```
SpaceBackend/
├── SpaceBackend/
│   ├── Controllers/          # API контроллеры
│   │   └── ServiceInfoController.cs
│   ├── Services/             # Бизнес-логика
│   │   ├── IServiceInfoService.cs
│   │   └── ServiceInfoService.cs
│   ├── Repositories/         # Доступ к данным
│   │   ├── IDatabaseRepository.cs
│   │   └── DatabaseRepository.cs
│   ├── Models/               # Модели данных
│   │   └── ServiceInfo.cs
│   ├── Program.cs            # Точка входа
│   └── appsettings.json      # Конфигурация
└── compose.yaml               # Docker Compose конфигурация
```

## Конфигурация

### Строка подключения к базе данных

Настройки подключения находятся в `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=postgres;Port=5432;Database=spacedb;Username=spaceuser;Password=spacepass"
  }
}
```

**Параметры:**
- `Host=postgres` - имя сервиса PostgreSQL из docker-compose
- `Port=5432` - порт PostgreSQL
- `Database=spacedb` - имя базы данных
- `Username=spaceuser` - пользователь БД
- `Password=spacepass` - пароль БД

### Изменение портов

Для изменения портов отредактируйте `compose.yaml`:

```yaml
ports:
  - "8080:8080"  # Формат: "хост:контейнер"
```

## Устранение неполадок

### Проблема: Приложение не запускается

1. Проверьте, что порт 8080 не занят другим приложением
2. Проверьте логи: `docker-compose logs spacebackend`
3. Убедитесь, что Docker запущен

### Проблема: Ошибка подключения к базе данных

1. Убедитесь, что PostgreSQL контейнер запущен: `docker ps`
2. Проверьте healthcheck PostgreSQL: `docker logs space_postgres`
3. Проверьте строку подключения в `appsettings.json`
4. Дождитесь полной инициализации БД (может занять несколько секунд)

### Проблема: Swagger не открывается

1. Убедитесь, что приложение запущено: `docker ps`
2. Проверьте, что используется правильный порт (8080)
3. Попробуйте открыть напрямую: `http://localhost:8080/swagger/index.html`

## Примеры использования

### Получение информации о сервисе (JavaScript)

```javascript
fetch('http://localhost:8080/api/ServiceInfo')
  .then(response => response.json())
  .then(data => {
    console.log('Service:', data.serviceName);
    console.log('Version:', data.version);
    console.log('DB Connected:', data.databaseInfo.isConnected);
  });
```

### Получение информации о сервисе (C#)

```csharp
using var client = new HttpClient();
var response = await client.GetAsync("http://localhost:8080/api/ServiceInfo");
var content = await response.Content.ReadAsStringAsync();
var serviceInfo = JsonSerializer.Deserialize<ServiceInfo>(content);
```

## Дополнительная информация

- **Swagger JSON:** `http://localhost:8080/swagger/v1/swagger.json`
- **Health Check:** Используйте `/api/ServiceInfo` для проверки состояния системы
- **База данных:** PostgreSQL 16 Alpine
- **.NET версия:** 8.0

## Контакты и поддержка

При возникновении проблем проверьте логи контейнеров и убедитесь, что все сервисы запущены корректно.
