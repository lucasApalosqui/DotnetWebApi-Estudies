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

        //endpoint que adiciona um novo produto via json atráves do body
        app.MapPost("/products", (Product product) =>{
           ProductRepository.addProduct(product);
           return Results.Created($"/products/{product.Name}", product.Name);
        });

        //endpoint que recebe dados via code route
        //api.com.br/users/{code}
        app.MapGet("/products/{name}", ([FromRoute]string name) =>{
            var product = ProductRepository.GetBy(name);
            if(product != null){
                return Results.Ok(product);
            }else{
                return Results.NotFound("num achei naum");
            }
            
        });

        //endpoint que altera um produto
         app.MapPut("/products", (Product product) =>{
           var productSave = ProductRepository.GetBy(product.Name);
           productSave.Price = product.Price;
           productSave.Status = product.Status;
           return Results.Ok();
        });

        //endpoint que deleta um produto pelo seu nome
        app.MapDelete("/products/{name}", ([FromRoute]string name) =>{
          var productDelete = ProductRepository.GetBy(name);
          ProductRepository.Remove(productDelete);
          return Results.Ok();
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
    public static Product GetBy(string name){
        return products.FirstOrDefault(p => p.Name == name);
    }

    //função que deleta um produto da lista de produtos
    public static void Remove(Product product){
        products.Remove(product);
    }
}


