using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Portal.Controllers
{
    public class BaseExportGridController<T> : BaseSearchController<T>
    {
        protected IExportGridViewModel GetExportGridViewModel(IExportGridMetaInfo metaInfoModel, List<object> listOfItems,  CancellationToken cancellationToken, List<string> screenConditionalProperties = null) {

            
            IExportGridViewModel exportGridViewModel = new ExportGridViewModel();

            exportGridViewModel.exportGridList = new List<List<IExportGridModel>>();
            if (metaInfoModel == null)
                metaInfoModel = new ExportGridMetaInfo();

            exportGridViewModel.exportGridMetaInfo = metaInfoModel;

            if (listOfItems != null)
                if (listOfItems.Count > 0)
                    foreach (var item in listOfItems)
                    {
                        List<IExportGridModel> lst = GetReportObjectList(item, screenConditionalProperties);
                        if (lst != null)
                            exportGridViewModel.exportGridList.Add(lst);
                    }
                else
                {
                    Type type = listOfItems.GetType().GetGenericArguments()[0];
                    listOfItems.Add(Activator.CreateInstance(type));
                    //listOfItems.Add(new type());

                    ViewData["NoRecord"] = "True";
                    List<IExportGridModel> lst = GetReportObjectList(listOfItems[0], screenConditionalProperties);
                    if (lst != null)
                        exportGridViewModel.exportGridList.Add(lst);
                }                       
            return exportGridViewModel;

        }
        protected List<IExportGridModel> GetReportObjectList(object pObject, List<string> screenConditionalProperties)
        {
            List<IExportGridModel> reportObjectList = new List<IExportGridModel>();
            if (pObject != null)
            {
                foreach (var prop in pObject.GetType().GetProperties())
                {
                    //Generic extension method written for getting attribute
                    //***********Signature**************
                    //object.GetAttributeFrom<Attribute>(string nameOfProperty)
                    var attrOnProperty = pObject.GetAttributeFrom<DisplayAttribute>(prop.Name);
                    // Only show those properties on which display attribute is written
                    if (attrOnProperty != null)
                    {
                        int order = Convert.ToInt32(attrOnProperty.GetOrder());
                        // if order is greater than 0 only then show the property in the report
                        if (order > 0)
                        {
                            //if there's a property with some groupname then find it in the supplied list of strings
                            // if matched then list the propety in export grid else continue to next 
                            if (!string.IsNullOrEmpty(attrOnProperty.GetGroupName())) {
                                if (screenConditionalProperties==null || !screenConditionalProperties.Exists(a => a == attrOnProperty.GetGroupName())) {
                                    continue;
                                }
                            }                            
                                IExportGridModel reportObject = new ExportGridModel();
                                reportObject.HeaderTitle = attrOnProperty.GetName();
                                reportObject.Name = prop.Name;
                                reportObject.Order = Convert.ToInt32(attrOnProperty.GetOrder());
                                reportObject.Value = prop.GetValue(pObject);
                                reportObjectList.Add(reportObject);                            
                        }                        
                    }

                }
            }
            return reportObjectList;
        }

    }
}