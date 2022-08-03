using System.Collections.Generic;

namespace FoodFinder.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.JoinEntities = new HashSet<RecipeRestaurant>();
      this.JoinEntities2 = new HashSet<IngredientRecipe>();
    }

    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<RecipeRestaurant> JoinEntities { get; set; }
    public virtual ICollection<IngredientRecipe> JoinEntities2 { get; set; }
  }
}