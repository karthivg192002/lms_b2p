using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Payment;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface ISubscriptionService
    {
        Task Subscribe(SubscribeDto dto);
        Task<IEnumerable<Subscription>> GetSubscriptions(Guid userId);
    }
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<Subscription> _repo;
        private readonly IMapper _mapper;

        public SubscriptionService(IRepository<Subscription> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task Subscribe(SubscribeDto dto)
        {
            var entity = _mapper.Map<Subscription>(dto);

            entity.StartDate = DateTime.UtcNow;
            entity.EndDate = DateTime.UtcNow.AddMonths(6);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptions(Guid userId)
        {
            return await _repo.FindAsync(x => x.UserId == userId);
        }
    }
}
