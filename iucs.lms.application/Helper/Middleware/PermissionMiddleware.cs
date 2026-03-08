using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iucs.lms.application.Helper.Enums;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace iucs.lms.application.Helper.Middleware
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRepository<UserRole> userRoleRepo,
            IRepository<RoleMenu> roleMenuRepo, IRepository<Menu> menuRepo)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint == null)
            {
                await _next(context);
                return;
            }

            var permissionAttribute = endpoint.Metadata.GetMetadata<PermissionAttribute>();

            if (permissionAttribute == null)
            {
                await _next(context);
                return;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var userRole = (await userRoleRepo.FindAsync(x => x.UserId == userId)).FirstOrDefault();

            if (userRole == null)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("No role assigned");
                return;
            }

            var menu = (await menuRepo.FindAsync(x => x.Url == permissionAttribute.MenuUrl)).FirstOrDefault();

            if (menu == null)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Menu not found");
                return;
            }

            var roleMenu = (await roleMenuRepo.FindAsync(x =>
                x.RoleId == userRole.RoleId &&
                x.MenuId == menu.Id)).FirstOrDefault();

            if (roleMenu == null)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Permission denied");
                return;
            }

            bool allowed = permissionAttribute.Permission switch
            {
                PermissionType.Create => roleMenu.CanCreate,
                PermissionType.Read => roleMenu.CanRead,
                PermissionType.Update => roleMenu.CanUpdate,
                PermissionType.Delete => roleMenu.CanDelete,
                _ => false
            };

            if (!allowed)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Permission denied");
                return;
            }

            await _next(context);
        }
    }
}
