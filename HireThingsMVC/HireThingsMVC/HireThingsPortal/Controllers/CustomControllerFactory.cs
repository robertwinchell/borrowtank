using ASOL.HireThings.Model;
using ASOL.HireThings.Service;

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ASOL.HireThings.Portal
{
    public class CustomControllerFactory : IControllerFactory
    {
        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            IController controller = null;

            if (string.Compare(controllerName, EnumControllerName.Account.ToString(), true) == 0)
            {
                IBaseAsyncService<IHireThingsUser> service = new UserService();
                controller = new AccountController((IUserService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Error.ToString(), true) == 0)
            {
                IBaseAsyncService<IErrorViewModel> service = new ErrorService();
                controller = new ErrorController((IErrorService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Country.ToString(), true) == 0)
            {
                IBaseAsyncService<ICountryModel> service = new CountryService();
                controller = new CountryController((ICountryService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.CountrySearch.ToString(), true) == 0)
            {
                IBaseAsyncService<ICountrySearchViewModel> service = new CountrySearchService();
                controller = new CountrySearchController((ICountrySearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.LocationSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<ILocationSearchViewModel> service = new LocationSearchService();
                controller = new LocationSearchController((ILocationSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Location.ToString(), true) == 0)
            {
                IBaseAsyncService<ILocationModel> service = new LocationService();
                controller = new LocationController((ILocationService)service);
            }
           
            else if (string.Compare(controllerName, EnumControllerName.Theme.ToString(), true) == 0)
            {
                IBaseAsyncService<IThemeModel> service = new ThemeService();
                controller = new ThemeController((IThemeService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.ThemeSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IThemeSearchViewModel> service = new ThemeSearchService();
                controller = new ThemeSearchController((IThemeSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Category.ToString(), true) == 0)
            {
                IBaseAsyncService<ICategoryModel> service = new CategoryService();
                controller = new CategoryController((ICategoryService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.CategorySearch.ToString(), true) == 0)
            {
                IBaseAsyncService<ICategorySearchViewModel> service = new CategorySearchService();
                controller = new CategorySearchController((ICategorySearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.EmailServerSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IEmailServerSearchViewModel> service = new EmailServerSearchService();
                controller = new EmailServerSearchController((IEmailServerSearchService)service);
            }

            else if (string.Compare(controllerName, EnumControllerName.EmailServer.ToString(), true) == 0)
            {
                IBaseAsyncService<IEmailServerModel> service = new EmailServerService();
                controller = new EmailServerController((IEmailServerService)service);
            }

            else if (string.Compare(controllerName, EnumControllerName.UserSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IUserSearchViewModel> service = new UserSearchService();
                controller = new UserSearchController((IUserSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.User.ToString(), true) == 0)
            {
                IBaseAsyncService<IHireThingsUser> service = new UserService();
                controller = new UserController((IUserService)service);
            }
           
            else if (string.Compare(controllerName, EnumControllerName.SecurityQuestion.ToString(), true) == 0)
            {
                IBaseAsyncService<ISecurityQuestionModel> service = new SecurityQuestionService();
                controller = new SecurityQuestionController((ISecurityQuestionService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.SecurityInfo.ToString(), true) == 0)
            {
                IBaseAsyncService<ISecurityInfoViewModel> service = new SecurityInfoService();
                controller = new SecurityInfoController((ISecurityInfoService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.RoleSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IRoleSearchViewModel> service = new RoleSearchService();
                controller = new RoleSearchController((IRoleSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Role.ToString(), true) == 0)
            {
                IBaseAsyncService<IRoleModel> service = new RoleService();
                controller = new RoleController((IRoleService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.ObjectSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IObjectSearchViewModel> service = new ObjectSearchService();
                controller = new ObjectSearchController((IObjectSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Object.ToString(), true) == 0)
            {
                IBaseAsyncService<IObjectModel> service = new ObjectService();
                controller = new ObjectController((IObjectService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Dropdown.ToString(), true) == 0)
            {
                IBaseAsyncService<IDropdownModel> service = new DropdownService();
                controller = new DropdownController((IDropdownService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.RoleObject.ToString(), true) == 0)
            {
                IBaseAsyncService<IRoleObjectViewModel> service = new RoleObjectService();
                controller = new RoleObjectController((IRoleObjectService)service);
            }
            
            else if (string.Compare(controllerName, EnumControllerName.Home.ToString(), true) == 0)
            {
                IBaseAsyncService<IHomeModel> service = new HomeService();
                controller = new HomeController((HomeService)service);
            }
            
            else if (string.Compare(controllerName, EnumControllerName.ItemDelete.ToString(), true) == 0)
            {
                IBaseAsyncService<IItemDeleteModel> service = new ItemDeleteService();
                controller = new ItemDeleteController((ItemDeleteService)service);
            }
            else if(string.Compare(controllerName, EnumControllerName.WebApiRoleSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IWebApiRoleSearchViewModel> service = new WebApiRoleSearchService();
                controller = new WebApiRoleSearchController((IWebApiRoleSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.WebApiRole.ToString(), true) == 0)
            {
                IBaseAsyncService<IWebApiRoleModel> service = new WebApiRoleService();
                controller = new WebApiRoleController((IWebApiRoleService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.WebApiObjectSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IWebApiObjectSearchViewModel> service = new WebApiObjectSearchService();
                controller = new WebApiObjectSearchController((IWebApiObjectSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.WebApiObject.ToString(), true) == 0)
            {
                IBaseAsyncService<IWebApiObjectModel> service = new WebApiObjectService();
                controller = new WebApiObjectController((IWebApiObjectService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.WebApiUserSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IWebApiUserSearchViewModel> service = new WebApiUserSearchService();
                controller = new WebApiUserSearchController((IWebApiUserSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.WebApiUser.ToString(), true) == 0)
            {
                IBaseAsyncService<IWebApiUser> service = new WebApiUserService();
                controller = new WebApiUserController((IWebApiUserService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.WebAPIRoleObject.ToString(), true) == 0)
            {
                IBaseAsyncService<IWebAPIRoleObjectViewModel> service = new WebAPIRoleObjectService();
                controller = new WebAPIRoleObjectController((IWebAPIRoleObjectService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Main.ToString(), true) == 0)
            {
                IBaseAsyncService<IMainModel> service = new MainService();
                controller = new MainController((MainService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.Advertisement.ToString(), true) == 0)
            {
                IBaseAsyncService<IAdvertisementModel> service = new AdvertisementService();
                controller = new AdvertisementController((AdvertisementService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.PublicAdvertisement.ToString(), true) == 0)
            {
                IBaseAsyncService<IAdvertisementModel> service = new AdvertisementService();
                controller = new PublicAdvertisementController((AdvertisementService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.PublicAdvertisementSearch.ToString(), true) == 0)
            {
                IBaseAsyncService<IAdvertisementSearchViewModel> service = new AdvertisementSearchService();
                controller = new PublicAdvertisementSearchController((AdvertisementSearchService)service);
            }
            else if (string.Compare(controllerName, EnumControllerName.PublicAccount.ToString(), true) == 0)
            {
                IBaseAsyncService<IHireThingsUser> service = new UserService();
                controller = new PublicAccountController((UserService)service);
            }
            else
            {
                throw new HttpException(404, "Page not Found");
            }

        
            return controller;
        }

        public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(
           System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

    }

    public enum EnumControllerName
    {
        PublicAdvertisement,
        PublicAccount,
        PublicAdvertisementSearch,
        Advertisement,
        Account,
        Main,
        Alerts,
        Reports,
        Activity,
        Home,
        Error,
        LocationSearch,
        Location,
        Country,
        CountrySearch,
        Workstation,
        WorkstationSearch,
        Theme,
        ThemeSearch,
        ThermostatTypeSearch,
        ThermostatType,
        Category,
        CategorySearch,
        DeviceSearch,
        Device,
        MeasureUnitSearch,
        EmailGroupSearch,
        EmailGroup,
        EmailServer,
        EmailServerSearch,
        UserSearch,
        Group,
        User,
        GroupSearch,
        EscalationSearch,
        Escalation,
        SecurityQuestionSearch,
        SecurityQuestion,
        ProbeManufacturerSearch,
        ProbeManufacturer,
        SecurityInfo,
        RoleSearch,
        Role,
        ObjectSearch,
        Object,
        Dashboard,
        Dropdown,
        RoleObject,
        AlarmSoundSearch,
        AlarmSound,
        AlarmSearch,
        Alarm,
        CorrectiveActionList,
        CorrectiveActionListSearch,
        CorrectiveActionTypeSearch,
        MailList,
        MailListSearch,
        ItemDelete,
        DashboardErrorSearch,
        ServiceObjectSearch,
        TCPServer,
        TCPServerSearch,
        CACategory,
        CACategorySearch,
        CACorrectiveAction,
        CACorrectiveActionSearch,
        CARootCause,
        CARootCauseSearch,
        CASymptom,
        CASymptomSearch,
        CACorrectiveActionSetting,
        CACorrectiveActionSettingSearch,
        DeviceReport,
        WebApiRoleSearch,
        WebApiRole,
        WebApiObjectSearch,
        WebApiObject,
        WebApiUser,
        WebApiUserSearch,
        WebAPIRoleObject,

        DeviceResetSearch,
        ReadingSampleSearch,
        ReadingSample

    }   
}