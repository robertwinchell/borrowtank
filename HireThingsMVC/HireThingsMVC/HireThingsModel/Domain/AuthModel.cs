
namespace ASOL.HireThings.Model
{
    public class AuthModel : BaseModel , IAuthModel
    {
        public bool AllowWrite { get; set; }
        public bool AllowDelete { get; set; }
        public string ObjectName { get; set; }
        public string URL { get; set; }
        public bool IsWithoutSearch { get; set; }
        public bool IsPublicUser { get; set; }

    }
}


