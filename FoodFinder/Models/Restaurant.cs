using System.Collections.Generic;

namespace FoodFinder.Models
{
  public class Restaurant
  {
    public Restaurant()
    {
      this.JoinEntities = new HashSet<RecipeRestaurant>();
    }

    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<RecipeRestaurant> JoinEntities { get; set; }

  }
}