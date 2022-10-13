using AutoMapper;
using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.RequestModels;

namespace CatchSmartHeadHunter.Helpers;

public static class RequestConverterExtensions
{
    private static IMapper _mapper => CreateMapper();
    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CandidateRequest, Candidate>()
                .ForMember(dest => dest.Skills,
                    opt => opt.MapFrom(scr => scr.Skills.Select(s => new Skill(s))))
                .ForMember(dest=>dest.AppliedPositions, opt=>opt.Ignore())
                .ForMember(dest=>dest.Id, opt=>opt.Ignore());

            cfg.CreateMap<Candidate, CandidateRequestResponse>()
                .ForMember(dest => dest.ApliedPositions,
                    opt => opt
                        .MapFrom(scr => scr.AppliedPositions.Select(ap => ap.Position)))
                .ForMember(dest => dest.Skills,
                    opt => opt.MapFrom(scr => scr.Skills.Select(s => s.SkillName)));

            cfg.CreateMap<Position, PositionRequest>();
            cfg.CreateMap<Position, PositionRequestResponse>();
            cfg.CreateMap<PositionRequest, Position>()
                .ForMember(dest=>dest.Id,opt=>opt.Ignore());
            
            cfg.CreateMap<Company, CompanyRequestResponse>()
                .ForMember(dest => dest.OpenPositions,
                opt =>
                    opt.MapFrom(scr => scr.OpenPositions.Select(op => op.Position)));
            
            cfg.CreateMap<CompanyRequest, Company>()
                .ForMember(dest => dest.OpenPositions,
                    opt =>
                        opt.MapFrom(scr => scr.OpenPositions))
                .ForMember(dest=>dest.Id,opt=>opt.Ignore());
                
            cfg.CreateMap<CompanyPosition, PositionRequest>()
                .ForMember(dest => dest.PositionName, opt =>
                opt.MapFrom(scr =>
                    scr.Position.PositionName))
                .ForMember(dest => dest.Description, opt =>
                opt.MapFrom(scr =>
                    scr.Position.Description));
            
            cfg.CreateMap<PositionRequest, CompanyPosition>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(scr => scr))
                .ForMember(dest=>dest.Id,opt=>opt.Ignore());
        });
        config.AssertConfigurationIsValid();
        
        return config.CreateMapper();
    }

    // ============================== Positions ==============================
    
    public static PositionRequestResponse ToPositionRequest(this Position position)
    {
        return _mapper.Map<PositionRequestResponse>(position);
    }

    public static Position ToPosition(this PositionRequest positionRequest)
    {
        return _mapper.Map<Position>(positionRequest);
    }

    public static ICollection<PositionRequestResponse> ToPositionRequestList(this ICollection<Position> positions)
    {
        return _mapper.Map<ICollection<PositionRequestResponse>>(positions);
    }

    // ============================== Company ==============================

    public static CompanyRequestResponse ToCompanyRequest(this Company company)
    {
        return _mapper.Map<CompanyRequestResponse>(company);
    }

    public static Company ToCompany(this CompanyRequest companyRequest)
    {
        return _mapper.Map<Company>(companyRequest);
    }

    public static ICollection<CompanyRequestResponse> ToCompanyRequestList(this ICollection<Company> companyRequestsList)
    {
        return _mapper.Map<ICollection<CompanyRequestResponse>>(companyRequestsList);
    }

    // ============================== Candidate ==============================

    public static CandidateRequestResponse ToCandidateRequest(this Candidate candidate)
    {
        return _mapper.Map<CandidateRequestResponse>(candidate);
    }

    public static Candidate ToCandidate(this CandidateRequest candidateRequest)
    {
        return _mapper.Map<Candidate>(candidateRequest);
    }

    public static ICollection<CandidateRequestResponse> ToCandidateRequestList(this ICollection<Candidate> candidates)
    {
        return _mapper.Map<ICollection<CandidateRequestResponse>>(candidates);
    }
}
