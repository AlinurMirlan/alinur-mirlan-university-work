using AutoMapper;
using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;
using BudgetTracker.Models.ViewModels;

namespace BudgetTracker.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        private readonly ILogger<AutoMapperProfile> logger;
        private readonly IHttpContextAccessor contextAccessor;

        public AutoMapperProfile(IHttpContextAccessor contextAccessor, ILogger<AutoMapperProfile> logger)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            base.CreateMap<RegistrationVm, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(registrationModel => registrationModel.Email))
                .ForMember(u => u.AccountBalance, opt => opt.MapFrom(registrationModel => registrationModel.InitialBalance));
            base.CreateMap<EntryDto, Entry>()
                .ForMember(e => e.Tags, opt => opt.MapFrom(iDto => GetTags(iDto.StringTags)));
            base.CreateMap<CategoryDto, Category>();
            base.CreateMap<EntryRecDto, EntryRecurring>()
                .ForMember(e => e.Tags, opt => opt.MapFrom(iDto => GetTags(iDto.StringTags)));
        }

        private List<Tag> GetTags(string? stringTags)
        {
            var tagNames = stringTags?.Split(',') ?? Enumerable.Empty<string>();
            var incomeTags = new List<Tag>();
            var userId = contextAccessor.HttpContext?.User.GetUserId();
            if (userId is null)
            {
                logger.LogError("Attempt to get an anonymous User's income tags.");
                throw new InvalidOperationException("User hasn't logged id.");
            }

            var processedTags = new HashSet<string>();
            foreach (var tagName in tagNames)
            {
                var processedTagName = tagName.Trim().ToLowerInvariant();
                if (processedTags.Contains(processedTagName))
                {
                    continue;
                }

                processedTags.Add(processedTagName);
                incomeTags.Add(new Tag()
                {
                    TagName = processedTagName,
                    UserId = userId
                });
            }

            return incomeTags;
        }
    }
}
