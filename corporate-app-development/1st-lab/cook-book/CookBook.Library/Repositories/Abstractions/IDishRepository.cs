﻿using CookBook.Library.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Library.Repositories.Abstractions
{
    public interface IDishRepository
    {
        public int AddDish(Dish dish, params (Ingredient ingredient, float amount)[] ingredients);
        public void DeleteDish(int dishId);
        public IList<Dish> GetDishes();
    }
}
