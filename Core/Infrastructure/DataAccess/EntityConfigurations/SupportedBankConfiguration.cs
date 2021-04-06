using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.DataAccess.EntityConfigurations
{
    public class SupportedBankConfiguration : IEntityTypeConfiguration<SupportedBank>
    {
        public void Configure(EntityTypeBuilder<SupportedBank> builder)
        {
        }
    }
}