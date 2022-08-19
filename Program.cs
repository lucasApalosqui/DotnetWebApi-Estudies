namespace api6;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

public class Program
{
    static void Main(string[] args)
    {
        //Inicia e constroi a aplicação para rodar em servidor local
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        //endpoint simples para pegar informações
        app.MapGet("/", () => "Hello World!");

        //endpoint de postagem com informação json via lambda
        app.MapPost("/user", () => new { name = "Juanito nieves de la vaganza", Age = 67, Hobbie = "Exportar hipopotamos" });

        //endpoint com alteração de headers e get de informações imbutido
        app.MapGet("/user", (HttpResponse response) =>
        {
            response.Headers.Add("Teste", "Paozinho");
            return new { name = "Juanito nieves de la vaganza", Age = 67, Hobbie = "Exportar hipopotamos" };
        });

        //endpoint que adiciona um novo produto via json atráves do body
        app.MapPost("/Product", (Product product) =>{
            return "name: " + product.Name + " - " + product.Price.ToString("F2", CultureInfo.InvariantCulture) + " - " + product.Status;
        });

        //endpoint que recebe dados via query
        //api.com.br/users?datastart={date}&dataend={date}
        app.MapGet("/getProduct", ([FromQuery]string dateStart, [FromQuery]string dateEnd) =>{
            return dateStart + " - " + dateEnd;
        });

        //endpoint que recebe dados via code route
        //api.com.br/users/{code}
        app.MapGet("/getProduct/{code}", ([FromRoute]string code) =>{
            return code;
        });

        //endpoint que recebe dados via header da aplicação
        app.MapGet("/getProducts", (HttpRequest request) => {
            return request.Headers["product-id"].ToString();
        });



        

        app.Run();
    }
}

//Classe auxiliar para adicionar um novo produto
public class Product {
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? Status { get; set; }
}


