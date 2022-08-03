namespace FoodFinder.Models
{
  public class RecipeRestaurant
  {       
    public int RecipeRestaurantId { get; set; }
    public int RestaurantId { get; set; }
    public int RecipeId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual Recipe Recipe { get; set; }
  }
}