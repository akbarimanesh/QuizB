using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizB.Counfiguration
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(x => x.CardNumber).HasMaxLength(16);
            builder.Property(x => x.HolderName).HasMaxLength(50);
            builder.Property(x => x.Password).HasMaxLength(12);
        }
    }
}
