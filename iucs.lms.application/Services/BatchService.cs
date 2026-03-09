using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Batch;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface IBatchService
    {
        Task<BatchDto> CreateAsync(CreateBatchDto dto);
        Task<IEnumerable<BatchDto>> GetAllAsync();
        Task AddStudent(AddBatchStudentDto dto);
        Task RemoveStudent(AddBatchStudentDto dto);
    }
    public class BatchService : IBatchService
    {
        private readonly IRepository<Batch> _batchRepo;
        private readonly IRepository<BatchStudent> _batchStudentRepo;
        private readonly IMapper _mapper;

        public BatchService(IRepository<Batch> batchRepo, IRepository<BatchStudent> batchStudentRepo,
            IMapper mapper)
        {
            _batchRepo = batchRepo;
            _batchStudentRepo = batchStudentRepo;
            _mapper = mapper;
        }

        public async Task<BatchDto> CreateAsync(CreateBatchDto dto)
        {
            var entity = _mapper.Map<Batch>(dto);

            await _batchRepo.AddAsync(entity);
            await _batchRepo.SaveChangesAsync();

            return _mapper.Map<BatchDto>(entity);
        }

        public async Task<IEnumerable<BatchDto>> GetAllAsync()
        {
            var batches = await _batchRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<BatchDto>>(batches);
        }

        public async Task AddStudent(AddBatchStudentDto dto)
        {
            var entity = new BatchStudent
            {
                Id = Guid.NewGuid(),
                BatchId = dto.BatchId,
                StudentId = dto.StudentId
            };

            await _batchStudentRepo.AddAsync(entity);
            await _batchStudentRepo.SaveChangesAsync();
        }

        public async Task RemoveStudent(AddBatchStudentDto dto)
        {
            var result = await _batchStudentRepo
                .FindAsync(x => x.BatchId == dto.BatchId && x.StudentId == dto.StudentId);

            var entity = result.FirstOrDefault()
                ?? throw new Exception("Student not in batch");

            _batchStudentRepo.Remove(entity);
            await _batchStudentRepo.SaveChangesAsync();
        }
    }
}
