// стартові дані
List<Person> users = new()
{
     new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37},
     new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41},
     new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24}
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", () => Results.Ok(users));

app.MapGet("/api/users/{id}", (string id) =>
{
    // отримуємо користовувача за id
    Person? user = users.FirstOrDefault(u => u.Id == id);
    // якщо не знайлено відправляємо статустний код і повідомлення про помилку
    if (user == null) return Results.NotFound(new { message = "Êîðèñòóâà÷ íå çíàéäåíèé" });

    // якщо користувача знайдено, то відправляємо його
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    // отримуємо користовувача за id
    Person? user = users.FirstOrDefault(u => u.Id == id);

    // якщо не знайлено відправляємо статустний код і повідомлення про помилку
    if (user == null) return Results.NotFound(new { message = "Êîðèñòóâà÷ íå çíàéäåíèé" });

    // якщо користувача знайдено, то видаляємо його
    users.Remove(user);
    return Results.Json(user);
});

app.MapPost("/api/users", (Person user) =>
{

    // встановлюємо id для нового користувача
    user.Id = Guid.NewGuid().ToString();
    // додаємо користувача в список
    users.Add(user);
    return user;
});

app.MapPut("/api/users", (Person userData) =>
{

    // отримуємо користовувача за id
    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    // якщо не знайлено відправляємо статустний код і повідомлення про помилку
    if (user == null) return Results.NotFound(new { message = "Êîðèñòóâà÷ íå çíàéäåíèé" });
    // якщо користувача знайдено, то мвняємо його дані
    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

app.Run();

public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

