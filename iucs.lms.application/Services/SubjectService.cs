using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Board;
using iucs.lms.application.DTOs.Subject;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();
        Task<SubjectDto> GetByIdAsync(Guid id);
        Task<SubjectDto> CreateAsync(CreateSubjectDto dto);
        Task<SubjectDto> UpdateAsync(Guid id, UpdateSubjectDto dto);
        Task DeleteAsync(Guid id);
    }
 
    public class SubjectService : ISubjectService
    {
        private readonly IRepository<Board> _repo;
        private readonly IMapper _mapper;

        public SubjectService(IRepository<Board> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            var boards = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<SubjectDto>>(boards);
        }

        public async Task<SubjectDto> GetByIdAsync(Guid id)
        {
            var board = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Board not found");

            return _mapper.Map<SubjectDto>(board);
        }

        public async Task<SubjectDto> CreateAsync(CreateSubjectDto dto)
        {
            var entity = _mapper.Map<Board>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<SubjectDto>(entity);
        }

        public async Task<SubjectDto> UpdateAsync(Guid id, UpdateSubjectDto dto)
        {
            var board = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Board not found");

            _mapper.Map(dto, board);

            _repo.Update(board);
            await _repo.SaveChangesAsync();

            return _mapper.Map<SubjectDto>(board);
        }

        public async Task DeleteAsync(Guid id)
        {
            var board = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Board not found");

            _repo.Remove(board);
            await _repo.SaveChangesAsync();
        }
    }
}
