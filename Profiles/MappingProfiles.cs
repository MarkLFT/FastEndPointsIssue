using AutoMapper;
using RM.Data.Svc.Finance;
using WebApplication1.Dto.Currencies;

namespace WebApplication1.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Currencies

        CreateMap<CurrencyCode, CurrencyListDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("Code", opt => opt.MapFrom(src => src.Code))
            .ForCtorParam("CurrencyName", opt => opt.MapFrom(src => src.CurrencyName))
            .ForCtorParam("CurrencySymbol", opt => opt.MapFrom(src => src.CurrencySymbol))
            .ForCtorParam("ActiveExchangeRate", opt => opt.MapFrom(src => src.ActiveCurrentRate))
            .ForSourceMember(x => x.CreatedOn, opt => opt.DoNotValidate());

        CreateMap<CurrencyCode, CurrencyDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("Code", opt => opt.MapFrom(src => src.Code))
            .ForCtorParam("IsoCode", opt => opt.MapFrom(src => src.IsoCode))
            .ForCtorParam("CurrencyName", opt => opt.MapFrom(src => src.CurrencyName))
            .ForCtorParam("CultureCode", opt => opt.MapFrom(src => src.CultureCode))
            .ForCtorParam("CurrencySymbol", opt => opt.MapFrom(src => src.CurrencySymbol))
            .ForCtorParam("PercentageIncrease", opt => opt.MapFrom(src => src.PercentageIncrease))
            .ForCtorParam("DecimalPlaces", opt => opt.MapFrom(src => src.DecimalPlaces))
            .ForSourceMember(x => x.CreatedOn, opt => opt.DoNotValidate());

        CreateMap<AddCurrencyDto, CurrencyCode>()
            .ForMember(x => x.CreatedOn, opt => opt.Ignore());

        CreateMap<UpdateCurrencyDto, CurrencyCode>()
            .ForMember(x => x.CreatedOn, opt => opt.Ignore());

        #endregion Currencies


    }
}
