using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizB.Entity;

namespace QuizB.Counfiguration
{
    public class UserConfig: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.NationalCode).HasMaxLength(10);
            builder.Property(x => x.Mobile).HasMaxLength(11);

            builder.HasData(new List<User>()
            {
                new User(){Id=1,Name="leila",NationalCode="0012345678",Mobile="09124640356" },
                 new User(){Id=2,Name="hana",NationalCode="0012387654",Mobile="09196043564" }

            });
        }
    }

}
