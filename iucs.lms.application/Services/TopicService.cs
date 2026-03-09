using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Board;
using iucs.lms.application.DTOs.Topic;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicDto>> GetAllAsync();
        Task<TopicDto> GetByIdAsync(Guid id);
        Task<TopicDto> CreateAsync(CreateTopicDto dto);
        Task<TopicDto> UpdateAsync(Guid id, UpdateTopicDto dto);
        Task DeleteAsync(Guid id);
    }
    public class TopicService : ITopicService
    {
        private readonly IRepository<Board> _repo;
        private readonly IMapper _mapper;

        public TopicService(IRepository<Board> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopicDto>> GetAllAsync()
        {
            var boards = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TopicDto>>(boards);
        }

        public async Task<TopicDto> GetByIdAsync(Guid id)
        {
            var board = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Board not found");

            return _mapper.Map<TopicDto>(board);
        }

        public async Task<TopicDto> CreateAsync(CreateTopicDto dto)
        {
            var entity = _mapper.Map<Board>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<TopicDto>(entity);
        }

        public async Task<TopicDto> UpdateAsync(Guid id, UpdateTopicDto dto)
        {
            var board = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Board not found");

            _mapper.Map(dto, board);

            _repo.Update(board);
            await _repo.SaveChangesAsync();

            return _mapper.Map<TopicDto>(board);
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
