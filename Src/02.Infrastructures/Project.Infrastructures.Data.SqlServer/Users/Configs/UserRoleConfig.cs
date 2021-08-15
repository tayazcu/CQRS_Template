using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Core.Domain.Base;
using Project.Core.Domain.Users.Entities;
using Project.Framework.Domain;
using System;

namespace Project.Infrastructures.Data.SqlServer.Users.Config
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(c => c.StatusType).IsRequired();
            builder.Property(c => c.StatusType).HasMaxLength(50).HasConversion(v => v.ToString(), v => (TypeOfStatus)Enum.Parse(typeof(TypeOfStatus), v));
        }
    }
}
