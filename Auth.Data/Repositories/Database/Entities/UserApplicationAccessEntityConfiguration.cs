using Auth.Core.Models;
using Auth.Core.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Repositories.Database.Entities
{
    public class UserApplicationAccessEntityConfiguration : IEntityTypeConfiguration<UserApplicationAccess>
    {
        public void Configure(EntityTypeBuilder<UserApplicationAccess> builder)
        {
            builder.HasKey(x => new
            {
                x.UserId,
                x.ApplicationClientId
            });

            builder.HasMany(x => x.UserApplicationCodeRequests)
                .WithOne(x => x.ApplicationAccess)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.UserApplicationSessions)
                .WithOne(x => x.ApplicationAccess)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}