using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface IBaseService<TContextObject, TCreateDTO, TGetDTO>
    {
        Task<int> CreateAsync(TCreateDTO dto);        
        Task RemoveAsync(int id);
        Task<TContextObject> GetAsync(int id);
        Task<TGetDTO> GetDTOAsync(int id);
    }
}
