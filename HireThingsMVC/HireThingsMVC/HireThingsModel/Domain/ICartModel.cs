using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface ICartModel
    {
        
        int Quantity { get; set; }
        long AdvertisementId { get; set; }
        string AdvertisementName { get; set; }
        double ItemPrice { get; set; }
        
        long AdvertiserId { get; set; }
        string AdvertiserName { get; set; }
        
    }

    public class CartModel : ICartModel
    {

        public int Quantity { get; set; }
        public long AdvertisementId { get; set; }
        public string AdvertisementName { get; set; }
        public double ItemPrice { get; set; }

        public long AdvertiserId { get; set; }
        public string AdvertiserName { get; set; }

        
    }
}
