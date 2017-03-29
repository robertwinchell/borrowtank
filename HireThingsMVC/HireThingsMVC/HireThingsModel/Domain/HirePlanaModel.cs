using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
   public class HirePlanaModel:BaseModel,IHirePlanaModel
    {
        public long HirePlanaId { get; set; }
        public string CartId { get; set; }
        public string Name { get; set; }
        public List<ICartModel> CartList { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdateDate { get; set; }       
    }
}
