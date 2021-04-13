using Auth.Core.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Repositories.Database.Entities
{
    public class UserApplicationCodeRequestEntityConfiguration : IEntityTypeConfiguration<UserApplicationCodeRequest>
    {
        public void Configure(EntityTypeBuilder<UserApplicationCodeRequest> builder)
        {
            builder.HasKey(x => x.Code);
        }
    }
}