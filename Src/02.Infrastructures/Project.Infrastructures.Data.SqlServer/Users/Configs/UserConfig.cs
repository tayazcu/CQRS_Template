using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Core.Domain.Users.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Infrastructures.Data.SqlServer.Users.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(x => x.UserName != "Admin123");
        }
    }
}
