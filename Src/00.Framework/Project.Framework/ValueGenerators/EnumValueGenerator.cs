using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Project.Framework.ValueGenerators
{
    public class EnumValueGenerator : ValueGenerator<Enum>
    {
        public override bool GeneratesTemporaryValues => false;

        public override Enum Next([NotNullAttribute] EntityEntry entry)
        {
            var s = (Type)entry.Entity;


            throw new NotImplementedException();
        }
    }
}
