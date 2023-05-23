﻿using AutoMapper;
using BudgetTracker.Models;
using BudgetTracker.Models.ViewModels;

namespace BudgetTracker.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            base.CreateMap<RegistrationViewModel, User>()
                .ForMember(user => user.UserName, options => options.MapFrom(userModel => userModel.Email));
        }
    }
}
