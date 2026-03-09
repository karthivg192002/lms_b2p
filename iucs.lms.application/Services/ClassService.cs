using AutoMapper;
using iucs.lms.application.DTOs.Board;
using iucs.lms.application.DTOs.Class;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDto>> GetAllAsync();
        Task<ClassDto> GetByIdAsync(Guid id);
        Task<ClassDto> CreateAsync(CreateClassDto dto);
        Task<ClassDto> UpdateAsync(Guid id, UpdateClassDto dto);
        Task DeleteAsync(Guid id);
    }
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> _repo;
        private readonly IMapper _mapper;

        public ClassService(IRepository<Class> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassDto>> GetAllAsync()
        {
            var classes = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassDto>>(classes);
        }

        public async Task<ClassDto> GetByIdAsync(Guid id)
        {
            var classResult = await _repo.GetByIdAsync(id)
                ?? throw new Exception("classResult not found");

            return _mapper.Map<ClassDto>(classResult);
        }

        public async Task<ClassDto> CreateAsync(CreateClassDto dto)
        {
            var entity = _mapper.Map<Class>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<ClassDto>(entity);
        }

        public async Task<ClassDto> UpdateAsync(Guid id, UpdateClassDto dto)
        {
            var classResult = await _repo.GetByIdAsync(id)
                ?? throw new Exception("classResult not found");

            _mapper.Map(dto, classResult);

            _repo.Update(classResult);
            await _repo.SaveChangesAsync();

            return _mapper.Map<ClassDto>(classResult);
        }

        public async Task DeleteAsync(Guid id)
        {
            var classResult = await _repo.GetByIdAsync(id)
                ?? throw new Exception("classResult not found");

            _repo.Remove(classResult);
            await _repo.SaveChangesAsync();
        }
    }
}
