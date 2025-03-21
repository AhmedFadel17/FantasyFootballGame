

namespace FantasyFootballGame.Application.DTOs.Errors
{
    public record ErrorResponseDto
    {
        public int StatusCode { get; set; }
        public required string Message { get; set; } 
        public string? Details { get; set; }
    }
}
