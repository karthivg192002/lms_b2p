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
    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentDto dto);
    }
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<PaymentTransaction> _repo;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<PaymentTransaction> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreatePayment(CreatePaymentDto dto)
        {
            var entity = _mapper.Map<PaymentTransaction>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
