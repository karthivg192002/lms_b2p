using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Course;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(Guid id);
        Task<CourseDto> CreateAsync(CreateCourseDto dto);
        Task<CourseDto> UpdateAsync(Guid id, UpdateCourseDto dto);
        Task DeleteAsync(Guid id);
    }
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _repo;
        private readonly IMapper _mapper;

        public CourseService(IRepository<Course> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetByIdAsync(Guid id)
        {
            var course = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Course not found");

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<CourseDto>(entity);
        }

        public async Task<CourseDto> UpdateAsync(Guid id, UpdateCourseDto dto)
        {
            var course = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Course not found");

            _mapper.Map(dto, course);

            _repo.Update(course);
            await _repo.SaveChangesAsync();

            return _mapper.Map<CourseDto>(course);
        }

        public async Task DeleteAsync(Guid id)
        {
            var course = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Course not found");

            _repo.Remove(course);
            await _repo.SaveChangesAsync();
        }
    }
}
