using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

//add services to the container
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(Options =>
{
    Options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(e => e.Value !=null && e.Value.Errors.Count > 0)
                .Select(e => new
                {
                    Field = e.Key,
                    Errors = e.Value!=null ? e.Value.Errors.Select(x => x.ErrorMessage).ToArray() : new string[0]

                }).ToList();

        

        return new BadRequestObjectResult(new
        {
            Message = "Validation Failed",
            Errors = errors
        });

    };

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();
app.MapGet("/", () => "Wroking fine for raihan");




app.Run();



// CRUD -> 
// create( create a category -> POST : / api . categories)
// read (read a category -> GET : / api . categories)
// update (update a category -> PUT : / api . categories)
// delete (delete a category -> DELETE : / api . categories)


// MVC -> Model View Controller
// 