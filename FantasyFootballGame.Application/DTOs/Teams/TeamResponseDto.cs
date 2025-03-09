using FantasyFootballGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FantasyFootballGame.Application.DTOs.Teams
{
    public record TeamResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string MainColor { get; set; }
        public string SecondaryColor { get; set; }
        public string ShirtImgSrc { get; set; }
    }
}
