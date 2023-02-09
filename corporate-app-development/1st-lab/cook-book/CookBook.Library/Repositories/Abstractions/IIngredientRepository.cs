using CookBook.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Repositories.Abstractions
{
    public interface IIngredientRepository
    {
        public int AddIngredient(Ingredient ingredient);
        public void DeleteIngredient(int ingredientId);
        public IList<Ingredient> GetDishIngredients(int dishId);
    }
}
