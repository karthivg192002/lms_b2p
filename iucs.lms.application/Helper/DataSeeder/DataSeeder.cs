using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Helper.DataSeeder
{
    public class DataSeeder
    {
        public static async Task SeedAsync(IRepository<Role> roleRepo, IRepository<Menu> menuRepo,
            IRepository<User> userRepo, IRepository<UserRole> userRoleRepo, IRepository<RoleMenu> roleMenuRepo)
        {
            var roles = await roleRepo.GetAllAsync();

            if (roles.Any())
                return;

            var adminRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Description = "Super Administrator",
                CreatedAt = DateTime.UtcNow
            };

            var teacherRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Teacher",
                Description = "Teacher Role",
                CreatedAt = DateTime.UtcNow
            };

            var studentRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Student",
                Description = "Student Role",
                CreatedAt = DateTime.UtcNow
            };

            await roleRepo.AddRangeAsync(new[] { adminRole, teacherRole, studentRole });
            await roleRepo.SaveChangesAsync();

            var dashboard = new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Dashboard",
                Url = "/dashboard",
                Icon = "dashboard",
                Sequence = 1,
                IsVisible = true
            };

            var users = new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Users",
                Url = "/users",
                Icon = "people",
                Sequence = 2,
                IsVisible = true
            };

            var rolesMenu = new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Roles",
                Url = "/roles",
                ParentId = users.Id,
                Sequence = 1,
                IsVisible = true
            };

            var permissions = new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Permissions",
                Url = "/permissions",
                ParentId = users.Id,
                Sequence = 2,
                IsVisible = true
            };

            var courses = new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Courses",
                Url = "/courses",
                Icon = "book",
                Sequence = 3,
                IsVisible = true
            };

            var students = new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Students",
                Url = "/students",
                Icon = "school",
                Sequence = 4,
                IsVisible = true
            };

            var teachers = new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Teachers",
                Url = "/teachers",
                Icon = "person",
                Sequence = 5,
                IsVisible = true
            };

            await menuRepo.AddRangeAsync(new[]
            {
                dashboard, 
                users, 
                rolesMenu,
                permissions,
                courses,
                students,
                teachers
            });

            await menuRepo.SaveChangesAsync();

            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                FirstName = "Karthikeyan",
                LastName = "V G",
                Email = "karthikeyanvg19@gmail.com",
                Phone = "9999999999",
                UserType = "Admin",
                PasswordHash = "$2a$12$z7BneaO4s34doXkW.nFbkucodV49fnka2y5.zY6WTNT7EsIC6LDhm",
                CreatedAt = DateTime.UtcNow
            };

            var teacherUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "teacher",
                FirstName = "John",
                LastName = "Teacher",
                Email = "teacher@lms.com",
                Phone = "8888888888",
                UserType = "Teacher",
                PasswordHash = "$2a$12$z7BneaO4s34doXkW.nFbkucodV49fnka2y5.zY6WTNT7EsIC6LDhm",
                CreatedAt = DateTime.UtcNow
            };

            await userRepo.AddRangeAsync(new[] { adminUser, teacherUser });
            await userRepo.SaveChangesAsync();

            await userRoleRepo.AddRangeAsync(new[]
            {
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                },
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = teacherUser.Id,
                    RoleId = teacherRole.Id
                }
            });

            await userRoleRepo.SaveChangesAsync();

            var menus = await menuRepo.GetAllAsync();

            foreach (var menu in menus)
            {
                await roleMenuRepo.AddAsync(new RoleMenu
                {
                    Id = Guid.NewGuid(),
                    RoleId = adminRole.Id,
                    MenuId = menu.Id,
                    CanCreate = true,
                    CanRead = true,
                    CanUpdate = true,
                    CanDelete = true
                });
            }

            await roleMenuRepo.AddAsync(new RoleMenu
            {
                Id = Guid.NewGuid(),
                RoleId = teacherRole.Id,
                MenuId = dashboard.Id,
                CanRead = true
            });

            await roleMenuRepo.AddAsync(new RoleMenu
            {
                Id = Guid.NewGuid(),
                RoleId = teacherRole.Id,
                MenuId = courses.Id,
                CanCreate = true,
                CanRead = true,
                CanUpdate = true
            });

            await roleMenuRepo.SaveChangesAsync();
        }
    }
}
