using Core.Application.Features.DocType.Command;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IDocTypeRepository
    {
        Task<Result<int>> DeleteDocTypeAsync(DeleteDocTypeCommand delete, CancellationToken cancellationToken);
    }
}
