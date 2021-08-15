using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Project.Core.Domain.Base;
using Project.Core.Domain.Users.Entities;
using Project.Framework.ValueGenerators;
using Project.Infrastructures.Data.SqlServer.Common;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Project.Infrastructures.Data.SqlServer.Users.Config
{
    public class UserInformationConfig : IEntityTypeConfiguration<UserInformation>
    {
        public void Configure(EntityTypeBuilder<UserInformation> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.UserType).HasMaxLength(50).HasConversion(v => v.ToString(), v => (TypeOfUser)Enum.Parse(typeof(TypeOfUser), v));

            builder.Property(c => c.PersianFirstName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(c => c.PersianLastName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(c => c.EnglishFirstName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.EnglishLastName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.PersianFatherName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.EnglishFatherName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.PersianPlaceOfBirth).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.Address).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.EnglishPlaceOfBirth).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.DateOfBirth).HasColumnType("datetime2").IsRequired(false);
            builder.Property(c => c.DateOfPassportIssuance).HasColumnType("datetime2").IsRequired(false);
            builder.Property(c => c.PassportExpirationDate).HasColumnType("datetime2").IsRequired(false);
            builder.Property(c => c.NationalCode).HasColumnType("char(10)").HasMaxLength(10).IsRequired(false);
            builder.Property(c => c.Gender).HasColumnType("char(1)").HasMaxLength(1).IsRequired(false);
            builder.Property(c => c.PhoneNumber).HasColumnType("char(11)").HasMaxLength(11).IsRequired(false);
            builder.Property(c => c.PostalCode).HasColumnType("char(10)").HasMaxLength(10).IsRequired(false);
            builder.Property(c => c.WhatsAppNumber).HasColumnType("char(11)").HasMaxLength(11).IsRequired(false);
            builder.Property(c => c.Mobile).HasColumnType("char(11)").HasMaxLength(11).IsRequired();
            builder.Property(c => c.ProfileImage).HasColumnType("varchar").HasMaxLength(500).IsRequired(false);
            builder.Property(c => c.PassportImage).HasColumnType("varchar").HasMaxLength(500).IsRequired(false);
            builder.Property(c => c.VisaImage).HasColumnType("varchar").HasMaxLength(500).IsRequired(false);
            builder.Property(c => c.Email).HasColumnType("varchar").HasMaxLength(200).IsRequired(false);
            builder.Property(c => c.PassportNumber).HasColumnType("varchar").HasMaxLength(9).IsRequired(false);

            builder.
               HasOne(x => x.ReferralUser).
               WithMany(x => x.ReferralUserInformations).
               HasForeignKey(x => x.ReferralUserId).
               OnDelete(DeleteBehavior.Cascade).
               IsRequired(false);

            builder.
               HasOne(x => x.User).
               WithOne(x => x.UserInformation).
               HasForeignKey<UserInformation>(x => x.UserId).
               OnDelete(DeleteBehavior.Cascade).
               IsRequired();
        }
    }
}
