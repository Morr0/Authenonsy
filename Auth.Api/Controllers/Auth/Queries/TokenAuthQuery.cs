using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Api.Controllers.Auth.Queries
{
    public class TokenAuthQuery
    {
        [JsonPropertyName("grant_type"), Required] public string GrantType { get; set; }
        [JsonPropertyName("client_id"), Required] public string ClientId { get; set; }
    }
}