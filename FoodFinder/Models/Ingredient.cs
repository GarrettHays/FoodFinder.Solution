using System.Collections.Generic;
using System;

namespace FoodFinder.Models
{
  public class Ingredient
  {
    public Ingredient()
    {
      this.JoinEntities = new HashSet<IngredientRecipe>();
    }

    public int IngredientId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<IngredientRecipe> JoinEntities { get; set; }
  }
}