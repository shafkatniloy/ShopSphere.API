var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/", () => "Wroking fine" );


//   products scope---

    var products = new List<Product>()
    {
        new Product (1, "samsung",1000 ),
        new Product (2, "iphone",2000 ),
        new Product (3, "oneplus",1500 ),
        new Product (4, "xiaomi",8000 )
    };

    
app.MapGet("/products", () =>
{

    
   return Results.Ok(products);

});


app.Run();

public record Product(int Id, string Name, decimal Price);

