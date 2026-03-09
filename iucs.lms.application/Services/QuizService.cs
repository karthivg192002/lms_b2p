using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Quiz;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface IQuizService
    {
        Task<QuizDto> CreateQuiz(CreateQuizDto dto);
        Task AddQuestion(CreateQuizQuestionDto dto);
        Task SubmitAttempt(QuizAttemptDto dto);
        Task<int> GetResult(Guid quizId, Guid studentId);
    }
    public class QuizService : IQuizService
    {
        private readonly IRepository<Quiz> _quizRepo;
        private readonly IRepository<QuizQuestion> _questionRepo;
        private readonly IRepository<QuizAttempt> _attemptRepo;
        private readonly IMapper _mapper;

        public QuizService(
            IRepository<Quiz> quizRepo,
            IRepository<QuizQuestion> questionRepo,
            IRepository<QuizAttempt> attemptRepo,
            IMapper mapper)
        {
            _quizRepo = quizRepo;
            _questionRepo = questionRepo;
            _attemptRepo = attemptRepo;
            _mapper = mapper;
        }

        public async Task<QuizDto> CreateQuiz(CreateQuizDto dto)
        {
            var quiz = _mapper.Map<Quiz>(dto);

            await _quizRepo.AddAsync(quiz);
            await _quizRepo.SaveChangesAsync();

            return _mapper.Map<QuizDto>(quiz);
        }

        public async Task AddQuestion(CreateQuizQuestionDto dto)
        {
            var question = _mapper.Map<QuizQuestion>(dto);

            await _questionRepo.AddAsync(question);
            await _questionRepo.SaveChangesAsync();
        }

        public async Task SubmitAttempt(QuizAttemptDto dto)
        {
            var attempt = _mapper.Map<QuizAttempt>(dto);

            await _attemptRepo.AddAsync(attempt);
            await _attemptRepo.SaveChangesAsync();
        }

        public async Task<int> GetResult(Guid quizId, Guid studentId)
        {
            var result = await _attemptRepo.FindAsync(x =>
                x.QuizId == quizId && x.StudentId == studentId);

            return result.FirstOrDefault()?.Score ?? 0;
        }
    }
}
