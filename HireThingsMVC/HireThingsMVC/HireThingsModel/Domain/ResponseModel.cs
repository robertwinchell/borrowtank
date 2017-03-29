using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public class ResponseModel: BaseModel,IResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public string retURL { get; set; }
    }

    public interface IResponseModel : IBaseModel
    {
         bool isSuccess { get; set; }
         string Message { get; set; }
         string retURL { get; set; }
    }
}
