using AutoMapper;
using FantasyFootballGame.Application.DTOs.Common;

namespace FantasyFootballGame.Application.Mapping
{
    public class PaginationConverter<TSource, TDestination>
    : ITypeConverter<PaginationSource<TSource>, PaginationDto<TDestination>>
    {
        private readonly IMapper _mapper;

        public PaginationConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PaginationDto<TDestination> Convert(
            PaginationSource<TSource> source,
            PaginationDto<TDestination> destination,
            ResolutionContext context)
        {
            return new PaginationDto<TDestination>
            {
                Items = _mapper.Map<List<TDestination>>(source.Items),
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount,
                HasPreviousPage = source.HasPreviousPage,
                HasNextPage = source.HasNextPage,
                TotalPages = source.TotalPages,
                ItemsCount = source.ItemsCount
            };
        }
    }


}
