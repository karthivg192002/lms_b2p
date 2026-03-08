using Microsoft.EntityFrameworkCore;
using iucs.lms.domain.Entities;

namespace iucs.lms.domain.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Core Auth & Users
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<RoleMenu> RoleMenus { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }

    // Curriculum
    public DbSet<Board> Boards { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Topic> Topics { get; set; }

    // Course & Batch
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseContent> CourseContents { get; set; }
    public DbSet<Batch> Batches { get; set; }
    public DbSet<BatchStudent> BatchStudents { get; set; }
    public DbSet<BatchTeacher> BatchTeachers { get; set; }
    public DbSet<LiveSession> LiveSessions { get; set; }

    // Library
    public DbSet<Book> Books { get; set; }

    // Exams
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<QuizAttempt> QuizAttempts { get; set; }

    // Analytics & Payment
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<RefundRequest> RefundRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite Keys setup
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<RoleMenu>()
            .HasKey(rm => new { rm.RoleId, rm.MenuId });
            
        modelBuilder.Entity<BatchStudent>()
            .HasKey(bs => new { bs.BatchId, bs.StudentId });
            
        modelBuilder.Entity<BatchTeacher>()
            .HasKey(bt => new { bt.BatchId, bt.TeacherId });

        // User email uniqueness
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
