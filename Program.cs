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


//   categories creation--
List<Category> categories = new List<Category>();




// read (read a category -> GET :  api/categories)
    
app.MapGet("/api/categories", () =>
{
   // return"heloo";
   return Results.Ok(categories);

});

//create( create a category -> POST : / api . categories)

app.MapPost("/api/categories", () =>
{
    var Category1 = new Category
    {
        CategoryId = Guid.Parse("ebed4657-d86e-4759-b955-d3d62c5ef6e5"),
        Name = "Smart phone",
        Description = "This is a smart phone category",
        CreatedAt = DateTime.UtcNow
    };
    categories.Add(Category1);

    return Results.Created($"/api.Categories/{Category1.CategoryId}",Category1);
});


// delete (delete a category -> DELETE : / api . categories)
app.MapDelete("/api/categories" , () => 
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("ebed4657-d86e-4759-b955-d3d62c5ef6e5"));
    
    if(foundCategory == null)
    {
        return Results.NotFound("category eith this id does not exist");
    }

    categories.Remove(foundCategory);
    return Results.NoContent();

});

// update (update a category -> PUT : / api . categories)
app.MapPut("/api/categories" , () => 
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("ebed4657-d86e-4759-b955-d3d62c5ef6e5"));
    
    if(foundCategory == null)
    {
        return Results.NotFound("category eith this id does not exist");
    }

    foundCategory.Name = "Smart phone of xiaomi ";
    foundCategory.Description = "This is a smart phone category of xiaomi";
    return Results.NoContent();

});


app.Run();

public record Category
{
  public Guid CategoryId {get; set;}
  public string? Name {get; set;}

  public string? Description {get; set;}

  public DateTime CreatedAt {get; set;}


};

// CRUD -> 
// create( create a category -> POST : / api . categories)
// read (read a category -> GET : / api . categories)
// update (update a category -> PUT : / api . categories)
// delete (delete a category -> DELETE : / api . categories)