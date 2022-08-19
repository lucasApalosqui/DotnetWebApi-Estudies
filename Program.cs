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
           ProductRepository.addProduct(product);
        });
        

        //endpoint que recebe dados via query
        //api.com.br/users?datastart={date}&dataend={date}
        app.MapGet("/getProduct", ([FromQuery]string dateStart, [FromQuery]string dateEnd) =>{
            return dateStart + " - " + dateEnd;
        });

        //endpoint que recebe dados via code route
        //api.com.br/users/{code}
        app.MapGet("/getProduct/{code}", ([FromRoute]string code) =>{
            var product = ProductRepository.GetBy(code);
            return product;
        });

        //endpoint que recebe dados via header da aplicação
        app.MapGet("/getProducts", (HttpRequest request) => {
            return request.Headers["product-id"].ToString();
        });

        //endpoint que altera um produto
         app.MapPut("/ProductUpdate", (Product product) =>{
           var productSave = ProductRepository.GetBy(product.Name);
           productSave.Price = product.Price;
           productSave.Status = product.Status;
        });

        //endpoint que deleta um produto pelo seu nome
        app.MapDelete("/ProductDelete/{name}", ([FromRoute]string name) =>{
          var productDelete = ProductRepository.GetBy(name);
          ProductRepository.Remove(productDelete);
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

//classe auxiliar com funções de alteração no endpoint
public static class ProductRepository{
    public static List<Product> products = new List<Product>();

    //função que adiciona um produto na lista de produtos
    public static void addProduct(Product product){
        if(products == null){
        products = new List<Product>();
        
        }
        products.Add(product);
    }

    //função que pega um produto especifico de acordo com o parametro indicado
    public static Product GetBy(string code){
        return products.First(p => p.Name == code);
    }

    //função que deleta um produto da lista de produtos
    public static void Remove(Product product){
        products.Remove(product);
    }
}


