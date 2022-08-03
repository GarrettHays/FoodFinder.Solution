using Microsoft.EntityFrameworkCore;

namespace FoodFinder.Models
{
  public class FoodFinderContext : DbContext
  {
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeRestaurant> RecipeRestaurant { get; set; }
    public DbSet<IngredientRecipe> IngredientRecipe { get; set; }

    public FoodFinderContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}