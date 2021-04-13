using Auth.Core.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Repositories.Database.Entities
{
    public class UserApplicationSessionEntityConfiguration : IEntityTypeConfiguration<UserApplicationSession>
    {
        public void Configure(EntityTypeBuilder<UserApplicationSession> builder)
        {
            builder.HasKey(x => x.AccessToken);
        }
    }
}