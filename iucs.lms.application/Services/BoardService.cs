using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Board;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardDto>> GetBoardsAsync();
        Task<BoardDto> GetBoardAsync(Guid id);
        Task<BoardDto> CreateBoardAsync(CreateBoardDto dto);
        Task<BoardDto> UpdateBoardAsync(Guid id, UpdateBoardDto dto);
        Task<bool> DeleteBoardAsync(Guid id);
    }
    public class BoardService : IBoardService
    {
        private readonly IRepository<Board> _repository;
        private readonly IMapper _mapper;

        public BoardService(IRepository<Board> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BoardDto>> GetBoardsAsync()
        {
            var boards = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BoardDto>>(boards);
        }

        public async Task<BoardDto> GetBoardAsync(Guid id)
        {
            var board = await _repository.GetByIdAsync(id);

            if (board == null)
                throw new Exception("Board not found");

            return _mapper.Map<BoardDto>(board);
        }

        public async Task<BoardDto> CreateBoardAsync(CreateBoardDto dto)
        {
            var board = _mapper.Map<Board>(dto);

            await _repository.AddAsync(board);
            await _repository.SaveChangesAsync();

            return _mapper.Map<BoardDto>(board);
        }

        public async Task<BoardDto> UpdateBoardAsync(Guid id, UpdateBoardDto dto)
        {
            var board = await _repository.GetByIdAsync(id);

            if (board == null)
                throw new Exception("Board not found");

            _mapper.Map(dto, board);

            _repository.Update(board);
            await _repository.SaveChangesAsync();

            return _mapper.Map<BoardDto>(board);
        }

        public async Task<bool> DeleteBoardAsync(Guid id)
        {
            var board = await _repository.GetByIdAsync(id);

            if (board == null)
                throw new Exception("Board not found");

            _repository.Remove(board);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
