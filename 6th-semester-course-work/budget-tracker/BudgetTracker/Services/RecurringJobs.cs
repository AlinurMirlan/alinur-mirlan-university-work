using AutoMapper;
using BudgetTracker.Models;
using BudgetTracker.Repositories;
using Hangfire;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BudgetTracker.Services
{
    public class RecurringJobs
    {
        private const string RecurringIncomeJobIdPrefix = "income";
        private const string RecurringExpenseJobIdPrefix = "expense";
        private readonly EntryRepository entryRepo;
        private readonly IMapper mapper;
        private readonly ILogger<RecurringJobs> logger;

        public RecurringJobs(EntryRepository entryRepo, IMapper mapper, ILogger<RecurringJobs> logger)
        {
            this.entryRepo = entryRepo;
            this.mapper = mapper;
            this.logger = logger;
        }

        public void AddRecurringEntryAsync(EntryRecurring entryRec, EntryName entryType)
        {
            if (entryRec.RecurringInterval <= 0)
            {
                throw new ArgumentException("Invalid recurring interval.");
            }

            string jobId = GetRecurringEntryJobId(entryRec.Id, entryType);
            RecurringJob.AddOrUpdate(jobId, () => AddEntryAsync(entryRec.Id, entryRec.StartDate, entryRec.EndDate, entryType, entryRec.RecurringInterval), "0 0 * * *");
        }

        public void RemoveRecurringEntry(int entryRecId, EntryName entryType)
        {
            string jobId = GetRecurringEntryJobId(entryRecId, entryType);
            RecurringJob.RemoveIfExists(jobId);
        }

        public async Task AddEntryAsync(int entryRecId, DateTime startDate, DateTime terminationDate, EntryName entryType, int recurringInterval)
        {
            if ((DateTime.Today - startDate.Date).Days % recurringInterval != 0)
            {
                return;
            }

            if (DateTime.Today >= terminationDate)
            {
                RemoveRecurringEntry(entryRecId, entryType);
                return;
            }

            var entryRec = await entryRepo.GetEntryRecAsync(entryRecId);
            if (entryRec is null)
            {
                logger.LogWarning("No {entryRec} is found with the given id {id} for the recurring job {job}", nameof(EntryRecurring), entryRecId, nameof(AddRecurringEntryAsync));
                return;
            }

            var entry = mapper.Map<Entry>(entryRec);
            await entryRepo.InsertEntryAsync(entry);
        }

        private string GetRecurringEntryJobId(int entryRecId, EntryName entryType)
        {
            string prefix = entryType == EntryName.Income ? RecurringIncomeJobIdPrefix : RecurringExpenseJobIdPrefix;
            return $"{prefix}{entryRecId}";
        }
    }
}
