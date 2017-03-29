using ASOL.HireThings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ASOL.HireThings.Core
{
    public class AdvertisementDAL : AbstractBaseDAL<IAdvertisementModel>
    {   
        #region async implementation
        public override async Task<long> SaveAsync(IAdvertisementModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            string csIds = "", csPrices = "";
            if (model.TimePrices != null && model.TimePrices.Count > 0) {
                foreach (ValueId par in model.TimePrices) {
                    csIds = csIds + par.Id + ",";
                    csPrices = csPrices + par.Price + ",";
                }
            }
         
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.Name.Length, PublicFunctions.ConvertNulltoDBNull(model.Name)));
            param.Add(DBObjects.SPParameter.Address, GetParameter(DBObjects.SPParameter.Address, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.Address.Length, PublicFunctions.ConvertNulltoDBNull(model.Address)));
            param.Add(DBObjects.SPParameter.PhoneNumber, GetParameter(DBObjects.SPParameter.PhoneNumber, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.PhoneNumber.Length, PublicFunctions.ConvertNulltoDBNull( model.PhoneNumber)));
            param.Add(DBObjects.SPParameter.Web, GetParameter(DBObjects.SPParameter.Web, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.Web.Length, PublicFunctions.ConvertNulltoDBNull(model.Web)));
            param.Add(DBObjects.SPParameter.Email, GetParameter(DBObjects.SPParameter.Email, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.Email.Length, PublicFunctions.ConvertNulltoDBNull(model.Email)));
            param.Add(DBObjects.SPParameter.AdvertiserId, GetParameter(DBObjects.SPParameter.AdvertiserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.AdvertiserId)));
            param.Add(DBObjects.SPParameter.CategoryId, GetParameter(DBObjects.SPParameter.CategoryId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.CategoryIds[0])));
            param.Add(DBObjects.SPParameter.LocationId, GetParameter(DBObjects.SPParameter.LocationId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.LocationId)));
            param.Add(DBObjects.SPParameter.VisibilityType, GetParameter(DBObjects.SPParameter.VisibilityType, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, PublicFunctions.ConvertNulltoDBNull(model.VisibilityType)));
            param.Add(DBObjects.SPParameter.SpecialTerms, GetParameter(DBObjects.SPParameter.SpecialTerms, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.SpecialTerms.Length, PublicFunctions.ConvertNulltoDBNull(model.SpecialTerms)));
            param.Add(DBObjects.SPParameter.FixedRateLabel, GetParameter(DBObjects.SPParameter.FixedRateLabel, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.FixedRateLabel.Length, PublicFunctions.ConvertNulltoDBNull(model.FixedRateLabel)));
            param.Add(DBObjects.SPParameter.FixedRateCharges, GetParameter(DBObjects.SPParameter.FixedRateCharges, ParameterDirection.Input, ((int)SqlDbType.Decimal), 16, PublicFunctions.ConvertNulltoDBNull(model.FixedRateCharges)));
            param.Add(DBObjects.SPParameter.DepositCharges, GetParameter(DBObjects.SPParameter.DepositCharges, ParameterDirection.Input, ((int)SqlDbType.Decimal), 16, PublicFunctions.ConvertNULL(model.DepositCharges,(Decimal)0)));
            param.Add(DBObjects.SPParameter.BondCharges, GetParameter(DBObjects.SPParameter.BondCharges, ParameterDirection.Input, ((int)SqlDbType.Decimal), 16, PublicFunctions.ConvertNULL(model.BondCharges,(Decimal)0)));
            param.Add(DBObjects.SPParameter.MinimumCharges, GetParameter(DBObjects.SPParameter.MinimumCharges, ParameterDirection.Input, ((int)SqlDbType.Decimal), 16, PublicFunctions.ConvertNULL(model.MinimumCharges,(Decimal)0)));
            param.Add(DBObjects.SPParameter.DefaultTimeChargingId, GetParameter(DBObjects.SPParameter.DefaultTimeChargingId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 16, PublicFunctions.ConvertNulltoDBNull(model.DefaultTimeChargingId)));
            param.Add(DBObjects.SPParameter.ChargingTypeId, GetParameter(DBObjects.SPParameter.ChargingTypeId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 16, PublicFunctions.ConvertNulltoDBNull(model.ChargingTypeId)));
            param.Add(DBObjects.SPParameter.IsVisibleToPublic, GetParameter(DBObjects.SPParameter.IsVisibleToPublic, ParameterDirection.Input, ((int)SqlDbType.Bit), 4, PublicFunctions.ConvertNulltoDBNull(Convert.ToBoolean(model.IsVisibleToPublic))));
            param.Add(DBObjects.SPParameter.Quantity, GetParameter(DBObjects.SPParameter.Quantity, ParameterDirection.Input, ((int)SqlDbType.Int), 16, PublicFunctions.ConvertNulltoDBNull(model.Quantity)));
            param.Add(DBObjects.SPParameter.IsMoreThanOne, GetParameter(DBObjects.SPParameter.IsMoreThanOne, ParameterDirection.Input, ((int)SqlDbType.Bit), 4, PublicFunctions.ConvertNulltoDBNull(Convert.ToBoolean(model.IsMoreThanOne))));
            param.Add(DBObjects.SPParameter.Description, GetParameter(DBObjects.SPParameter.Description, ParameterDirection.Input, ((int)SqlDbType.NVarChar), model.Description.Length, PublicFunctions.ConvertNulltoDBNull(model.Description)));
            param.Add(DBObjects.SPParameter.IdList, GetParameter(DBObjects.SPParameter.IdList, ParameterDirection.Input, ((int)SqlDbType.NVarChar), csIds.Length, PublicFunctions.ConvertNulltoDBNull(csIds)));

            param.Add(DBObjects.SPParameter.PriceList, GetParameter(DBObjects.SPParameter.PriceList, ParameterDirection.Input, ((int)SqlDbType.NVarChar), csPrices.Length, PublicFunctions.ConvertNulltoDBNull(csPrices)));
            param.Add(DBObjects.SPParameter.AddUserId, GetParameter(DBObjects.SPParameter.AddUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.AdvertisementId, GetParameter(DBObjects.SPParameter.AdvertisementId, ParameterDirection.Output, ((int)SqlDbType.BigInt), 8, 0));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.hspAdvertisementSave.ToString(), cancellationToken, param);

            return Convert.ToInt64(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);
        }

        public override async Task<bool> UpdateAsync(IAdvertisementModel model, CancellationToken cancellationToken)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add(DBObjects.SPParameter.AdvertisementId, GetParameter(DBObjects.SPParameter.AdvertisementId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.AdvertisementId));
            param.Add(DBObjects.SPParameter.Name, GetParameter(DBObjects.SPParameter.Name, ParameterDirection.Input, ((int)SqlDbType.VarChar), 100, model.Name));
            //param.Add(DBObjects.SPParameter.IsActive, GetParameter(DBObjects.SPParameter.IsActive, ParameterDirection.Input, ((int)SqlDbType.Bit), 1, model.IsActive));
            param.Add(DBObjects.SPParameter.UpdateUserId, GetParameter(DBObjects.SPParameter.UpdateUserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, model.UserId));
            param.Add(DBObjects.SPParameter.RetVal, GetParameter(DBObjects.SPParameter.RetVal, ParameterDirection.Output, ((int)SqlDbType.Int), 4, 0));

            await this.ExecuteSPAsync(DBObjects.StoredProcedures.hspAdvertisementUpdate.ToString(), cancellationToken, param);

            return Convert.ToBoolean(((SqlParameter)param[DBObjects.SPParameter.RetVal]).Value);

        }

        public override async Task<IAdvertisementModel> SelectByIDAsync(Int64 id, Int64 userId, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(DBObjects.SPParameter.AdvertisementId, GetParameter(DBObjects.SPParameter.AdvertisementId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, id));
            param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, userId));
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspAdvertisementSelectById.ToString(), cancellationToken, param);

            IAdvertisementModel model = new AdvertisementModel();
            model.AdvertisementId = Convert.ToInt64(dt.Rows[0]["AdvertisementId"]);
            model.Name = PublicFunctions.ConvertNULL(dt.Rows[0]["Name"],"");
            model.Description = PublicFunctions.ConvertNULL(dt.Rows[0]["Description"],"");
            model.CategoryName = PublicFunctions.ConvertNULL(dt.Rows[0]["CategoryName"],"");
            model.IsMoreThanOne = Convert.ToInt32(PublicFunctions.ConvertNULL(dt.Rows[0]["IsMoreThanOne"],false));
            model.Quantity = PublicFunctions.ConvertNULL(dt.Rows[0]["Quantity"],(int)0);
            model.IsVisibleToPublic = Convert.ToInt32(PublicFunctions.ConvertNULL(dt.Rows[0]["IsVisibleToPublic"],false));
            model.ChargingTypeId = PublicFunctions.ConvertNULL(dt.Rows[0]["ChargingTypeId"],(long)0);
            model.DefaultTimeChargingId = PublicFunctions.ConvertNULL(dt.Rows[0]["DefaultTimeChargingId"],(long)0);
            model.MinimumCharges = PublicFunctions.ConvertNULL(dt.Rows[0]["MinimumCharges"], (decimal)0.00);
            model.BondCharges = PublicFunctions.ConvertNULL(dt.Rows[0]["BondCharges"], (decimal)0.00);
            model.DepositCharges = PublicFunctions.ConvertNULL(dt.Rows[0]["DepositCharges"], (decimal)0.00);
            model.FixedRateCharges = PublicFunctions.ConvertNULL(dt.Rows[0]["FixedRateCharges"],(decimal)0.00);
            model.FixedRateLabel = PublicFunctions.ConvertNULL(dt.Rows[0]["FixedRateLabel"],"");
            model.SpecialTerms = PublicFunctions.ConvertNULL(dt.Rows[0]["SpecialTerms"],"");
            model.VisibilityType = PublicFunctions.ConvertNULL(dt.Rows[0]["VisibilityType"],(int)0);
            model.LocationId = PublicFunctions.ConvertNULL(dt.Rows[0]["LocationId"],(long)0);
            model.AdvertiserId = PublicFunctions.ConvertNULL(dt.Rows[0]["AdvertiserId"],(long)0);
            model.Email = PublicFunctions.ConvertNULL(dt.Rows[0]["Email"],"");
            model.Web = PublicFunctions.ConvertNULL(dt.Rows[0]["Web"],"");
            model.PhoneNumber = PublicFunctions.ConvertNULL(dt.Rows[0]["PhoneNumber"],"");
            model.Address = PublicFunctions.ConvertNULL(dt.Rows[0]["Address"],"");
            model.AddDate = PublicFunctions.ConvertNULL(dt.Rows[0]["AddDate"],DateTime.Now.AddYears(-100));
            model.ChargingDetail = PublicFunctions.ConvertNULL(dt.Rows[0]["ChargingDetail"],"");
            model.ChargingTypeName = PublicFunctions.ConvertNULL(dt.Rows[0]["ChargingTypeName"],"");
            return model;
        }

        public override async Task<List<IAdvertisementModel>> SelectAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<IAdvertisementModel>> SelectAllByFilterAsync(IPredicate predicate, CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
            string retVal = predicate.Serialize();

            DataTable dt;
            Dictionary<string, object> param = new Dictionary<string, object>();
            //pageInfo.ColumnName = (pageInfo.ColumnName == String.Empty) ?  "1" :  pageInfo.ColumnName;
            //string sortOrder = (pageInfo.SortType == 0) ? " asc " : (pageInfo.SortType == 1) ? " desc" : "";

            param.Add(DBObjects.SPParameter.Predicate, GetParameter(DBObjects.SPParameter.Predicate, ParameterDirection.Input, ((int)SqlDbType.NVarChar), retVal.Length, retVal));
            //param.Add(DBObjects.SPParameter.CPage, GetParameter(DBObjects.SPParameter.CPage, ParameterDirection.Input, ((int)SqlDbType.Int), 8, pageInfo.CPage));
            //param.Add(DBObjects.SPParameter.PSize, GetParameter(DBObjects.SPParameter.PSize, ParameterDirection.Input, ((int)SqlDbType.Int), 8, Convert.ToInt32((pageInfo.PSize))));
            //param.Add(DBObjects.SPParameter.UserId, GetParameter(DBObjects.SPParameter.UserId, ParameterDirection.Input, ((int)SqlDbType.BigInt), 8, pageInfo.UserId));
            //param.Add(DBObjects.SPParameter.ColumnName, GetParameter(DBObjects.SPParameter.ColumnName, ParameterDirection.Input, ((int)SqlDbType.NVarChar),pageInfo.ColumnName.Length+5, pageInfo.ColumnName+sortOrder));



            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspAdvertisementSelect.ToString(), cancellationToken, param);

            List<IAdvertisementModel> list = new List<IAdvertisementModel>();
            list = (from DataRow dr in dt.Rows
                    select new AdvertisementModel
                    {
                        AdvertisementId = Convert.ToInt64(dr["AdvertisementId"]), 
                        Name = dr["Name"].ToString(),
                        ChargingDetail = dr["ChargingDetail"].ToString(),
                        CategoryName = dr["CategoryName"].ToString(),
                        ChargingTypeName = dr["ChargingTypeName"].ToString(),
                        
                       // IsMoreThanOne = PublicFunctions.ConvertNULL(dr["IsMoreThanOne"],0),
                        //Quantity = PublicFunctions.ConvertNULL(dr["Quantity"], 0),
                        //IsVisibleToPublic = PublicFunctions.ConvertNULL(dr["IsVisibleToPublic"], 0),
                        //ChargingTypeId = PublicFunctions.ConvertNULL(dr["ChargingTypeId"], (long)0),
                        //DefaultTimeChargingId = PublicFunctions.ConvertNULL(dr["DefaultTimeChargingId"], (long)0),
                        //MinimumCharges = PublicFunctions.ConvertNULL(dr["MinimumCharges"], (decimal)0.00),
                        //BondCharges = PublicFunctions.ConvertNULL(dr["BondCharges"], (decimal)0.00),
                        //DepositCharges = PublicFunctions.ConvertNULL(dr["DepositCharges"], (decimal)0.00),
                        //FixedRateCharges= PublicFunctions.ConvertNULL(dr["FixedRateCharges"], (decimal)0.00),
                        //FixedRateLabel = PublicFunctions.ConvertNULL(dr["FixedRateLabel"],""),
                        //SpecialTerms = PublicFunctions.ConvertNULL(dr["SpecialTerms"],""),
                        //VisibilityType = PublicFunctions.ConvertNULL(dr["VisibilityType"], 0),
                        //LocationId = PublicFunctions.ConvertNULL(dr["LocationId"], (long)0),
                        //AdvertiserId = PublicFunctions.ConvertNULL(dr["AdvertiserId"], (long)0),
                        //Email = PublicFunctions.ConvertNULL(dr["Email"],""),
                        //Web = PublicFunctions.ConvertNULL(dr["Web"],""),
                        //PhoneNumber = PublicFunctions.ConvertNULL(dr["PhoneNumber"],""),
                        //Address = PublicFunctions.ConvertNULL(dr["Address"],"")
                 
                    }).ToList<IAdvertisementModel>();

            return list;
        }
        public  async Task<List<IAdvertisementModel>> SelectAllForHomePageAsync( CancellationToken cancellationToken, IPageInfo pageInfo = null)
        {
         
            DataTable dt;
            Dictionary<string, object> param = new Dictionary<string, object>();
           
            dt = await this.GetSPDataTableAsync(DBObjects.StoredProcedures.hspAdvertisementSelectForHomePage.ToString(), cancellationToken);

            List<IAdvertisementModel> list = new List<IAdvertisementModel>();
            list = (from DataRow dr in dt.Rows
                    select new AdvertisementModel
                    {
                        AdvertisementId = Convert.ToInt64(dr["AdvertisementId"]),
                        Name = dr["Name"].ToString(),
                        ChargingDetail = dr["ChargingDetail"].ToString(),
                        CategoryName = dr["CategoryName"].ToString(),
                        ChargingTypeName = dr["ChargingTypeName"].ToString(),
                        
                    }).ToList<IAdvertisementModel>();

            return list;
        }

        #endregion

    }
}
