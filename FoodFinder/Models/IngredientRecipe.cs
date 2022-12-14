namespace FoodFinder.Models
{
  public class IngredientRecipe
  {       
    public int IngredientRecipeId { get; set; }
    public int IngredientId { get; set; }
    public int RecipeId { get; set; }
    public virtual Ingredient Ingredient { get; set; }
    public virtual Recipe Recipe { get; set; }
  }
}