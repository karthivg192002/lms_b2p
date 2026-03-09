using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.LiveSession;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface ILiveSessionService
    {
        Task<LiveSessionDto> CreateAsync(CreateLiveSessionDto dto);
        Task<IEnumerable<LiveSessionDto>> GetByBatch(Guid batchId);
        Task<LiveSessionDto> UpdateStatus(Guid id, string status);
    }
    public class LiveSessionService : ILiveSessionService
    {
        private readonly IRepository<LiveSession> _repo;
        private readonly IMapper _mapper;

        public LiveSessionService(IRepository<LiveSession> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LiveSessionDto> CreateAsync(CreateLiveSessionDto dto)
        {
            var entity = _mapper.Map<LiveSession>(dto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<LiveSessionDto>(entity);
        }

        public async Task<IEnumerable<LiveSessionDto>> GetByBatch(Guid batchId)
        {
            var sessions = await _repo.FindAsync(x => x.BatchId == batchId);

            return _mapper.Map<IEnumerable<LiveSessionDto>>(sessions);
        }

        public async Task<LiveSessionDto> UpdateStatus(Guid id, string status)
        {
            var session = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Session not found");

            if (!Enum.TryParse<LiveSessionStatus>(status, true, out var parsedStatus))
                throw new Exception("Invalid session status");

            session.Status = parsedStatus;

            _repo.Update(session);
            await _repo.SaveChangesAsync();

            return _mapper.Map<LiveSessionDto>(session);
        }
    }
}
