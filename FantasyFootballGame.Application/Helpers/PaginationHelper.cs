using FantasyFootballGame.Application.DTOs.Common;

namespace FantasyFootballGame.Application.Helpers
{
    public static class PaginationHelper
    {
        public static PaginationDto<T> CreatePagedResult<T>(
            List<T> items,
            int pageNumber,
            int pageSize,
            int totalCount)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginationDto<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount
            };
        }        
    }
} 