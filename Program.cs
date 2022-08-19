//Inicia e constroi a aplicação para rodar em servidor local
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//endpoint simples para pegar informações
app.MapGet("/", () => "Hello World!");

//endpoint de postagem com informação json via lambda
app.MapPost("/user", () => new {name = "Juanito nieves de la vaganza", Age = 67, Hobbie = "Exportar hipopotamos"});

//endpoint com alteração de headers e get de informações imbutido
app.MapGet("/user", (HttpResponse response) => {
    response.Headers.Add("Teste", "Paozinho");
    return new {name = "Juanito nieves de la vaganza", Age = 67, Hobbie = "Exportar hipopotamos"};
});

app.Run();
