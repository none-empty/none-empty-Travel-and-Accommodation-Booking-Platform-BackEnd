using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.Queries;

public class GetHotelDetailedInfoQueryHandler : IRequestHandler<GetHotelDetailedInfoQuery,GetHotelDetailedInfoQueryResponse>
{
    private readonly IGetHotelDetailedInfo _getHotelDetailedInfo;
    
    public GetHotelDetailedInfoQueryHandler(IGetHotelDetailedInfo getHotelDetailedInfo)
    {
        _getHotelDetailedInfo = getHotelDetailedInfo;
    }

    public Task<GetHotelDetailedInfoQueryResponse> Handle(GetHotelDetailedInfoQuery request, CancellationToken cancellationToken)
    {
        return _getHotelDetailedInfo.Execute(request.Id);
    }
}