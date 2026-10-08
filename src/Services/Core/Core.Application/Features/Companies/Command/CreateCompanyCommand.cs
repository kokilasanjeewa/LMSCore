using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Core.Shared;
using MediatR;
using Core.Application.Interfaces.Repositories;

namespace Core.Application.Features.Companies.Command
{
    public class CreateCompanyCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.Company>
    {
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(60, ErrorMessage = "The company name cannot exceed 60 characters. ")]
        public string? ComName { get; set; }
        [Required]
        [Display(Name = "Company Code")]
        [StringLength(5, ErrorMessage = "The company code cannot exceed 5 characters. ")]
        public string? ComCode { get; set; }
        [Required]
        public int CntrySerialID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address1 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address2 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The address cannot exceed 60 characters. ")]
        public string? Address3 { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "The BR# cannot exceed 12 characters. ")]
        public string? BR { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The telephone cannot exceed 10 characters. ")]

        public string? Telephone1 { get; set; }

        [StringLength(10, ErrorMessage = "The telephone cannot exceed 10 characters. ")]
        public string? Telephone2 { get; set; }

        [StringLength(10, ErrorMessage = "The telephone cannot exceed 10 characters. ")]
        public string? Telephone3 { get; set; }

        [StringLength(10, ErrorMessage = "The mobile cannot exceed 10 characters. ")]
        public string? Mobile1 { get; set; }

        [StringLength(10, ErrorMessage = "The mobile cannot exceed 10 characters. ")]
        public string? Mobile2 { get; set; }
        [StringLength(10, ErrorMessage = "The fax cannot exceed 10 characters. ")]
        public string? Fax { get; set; }
        [StringLength(35, ErrorMessage = "The email cannot exceed 35 characters. ")]
        public string? Email { get; set; }
        public Uri? WebSite { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The registered address cannot exceed 60 characters. ")]
        public string? RegAdrs1 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The registered address cannot exceed 60 characters. ")]
        public string? RegAdrs2 { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The registered address cannot exceed 60 characters. ")]
        public string? RegAdrs3 { get; set; }
        public string? VAT { get; set; }
        public string? SVAT { get; set; }
        public string? NBT { get; set; }
        [StringLength(50, ErrorMessage = "The mobile cannot exceed 10 characters. ")]

        public string? CompanyLogoUrl { get; set; }
        [StringLength(50, ErrorMessage = "The mobile cannot exceed 10 characters. ")]

        public string? SmallComLogoUrl { get; set; }
        public byte? SOPayrollPeriodStartDay { get; set; }
        public byte? SOPayrollPeriodEndDay { get; set; }
        public byte? WBPayrollPeriodStartDay { get; set; }
        public byte? WBPayrollPeriodEndDay { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCompanyCommand, Domain.Entities.Company>();
        }
    }
    internal class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCompanyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<int>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            // Correct way
            CreateCompanyCommandValidator validator = new CreateCompanyCommandValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(command);

            //  Test error validation
            // _validator.ValidateAndThrow(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(messages: errors);
            }
            try
            {

                var carete = _mapper.Map<Company>(command);
                await _unitOfWork.Repository<Company>().AddAsync(carete);

                var param = _unitOfWork.Repository<TheNumber>().Entities.Where(p => p.TheNumberName == "Company").FirstOrDefault();
                param.LastNumber = param.LastNumber + 1;
                await _unitOfWork.Repository<TheNumber>().UpdateAsync(param,param.TheNumberSerialID);

                carete.AddDomainEvent(new CompanyCreatedEvent(carete));

                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(data: carete.ComSerialID, message: "Saved successfully");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<int>.FailureAsync(message: ex.Message + ex.InnerException);


            }
        }
    }
}