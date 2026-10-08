using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using Dapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Core.Application.Features.Users.Queries.GetBtnRptByMnuSeridsDynamicQuery
{
  
    public record GetBtnRptByMnuSeridsDynamicQuery : IRequest<Result<List<GetBtnRptByMnuSeridsDynamicDto>>>
    {
        [Required]
        public int? Action { get; set; }
        [Required]
        public int[] MnuSerialIDs { get; set; } 

        public GetBtnRptByMnuSeridsDynamicQuery(int[] mnuSerialIDs, int? action)
        {
            MnuSerialIDs = mnuSerialIDs;
            Action = action;
        }
    }

    internal class GetBtnRptByMnuSeridsDynamicQueryHandler : IRequestHandler<GetBtnRptByMnuSeridsDynamicQuery, Result<List<GetBtnRptByMnuSeridsDynamicDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;

        public GetBtnRptByMnuSeridsDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<Result<List<GetBtnRptByMnuSeridsDynamicDto>>> Handle(GetBtnRptByMnuSeridsDynamicQuery query, CancellationToken cancellationToken)
        {

            GetBtnRptByMnuSeridsCommandValidator validator = new GetBtnRptByMnuSeridsCommandValidator();

            // Perform validation
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                // Handle validation failures
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<List<GetBtnRptByMnuSeridsDynamicDto>>.FailureAsync(new List<GetBtnRptByMnuSeridsDynamicDto>(), errors);
            }
            var parameters = new DynamicParameters();
            parameters.Add("@action", query.Action, DbType.Int16, ParameterDirection.Input);
            // Convert array to comma-separated string
            var serialIdsString = string.Join(",", query.MnuSerialIDs);
            parameters.Add("@mnuIDs", serialIdsString, DbType.String, ParameterDirection.Input);

            var sp = "dbo.GetAllPermissionFilterByMnuIDs";

            var result = await _sqlDataAccess.LoadSPDataQuery<GetBtnRptByMnuSeridsDynamicDto, dynamic>(sp, parameters);
            return await Result<List<GetBtnRptByMnuSeridsDynamicDto>>.SuccessAsync(data:result.ToList(),message:"Success");
        }
    }

}
