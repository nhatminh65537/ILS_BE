using ILS_BE.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace ILS_BE.Infrastructure.SeedData
{
    public class PermissionSeedData : IEntityTypeConfiguration<Permission>
    {
        private List<Permission> _permissions { get; set; }

        public PermissionSeedData(ref List<Permission> permission)
        {
            _permissions = permission;
        }

        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)) && !t.IsAbstract)
                .ToList();
            var baseActions = typeof(ControllerBase)
                .GetMethods()
                .Select(m => m.Name)
                .ToList();
            int id = 1;
            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods()
                    .Where(m => !baseActions.Contains(m.Name))
                    .ToList();
                foreach (var action in actions)
                {
                    var permission = new Permission
                    {
                        Id = id,
                        Name = $"{controller.Name.Replace("Controller", "")}.{action.Name}",
                    };
                    builder.HasData(permission);
                    _permissions.Add(permission);
                    id++;
                }
            }
        }
    }
}
