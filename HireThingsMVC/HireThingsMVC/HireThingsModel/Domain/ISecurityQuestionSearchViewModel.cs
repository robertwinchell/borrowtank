﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOL.HireThings.Model
{
    public interface ISecurityQuestionSearchViewModel:IBaseModel
    {
        List<ISecurityQuestionModel> SecurityQuestionList { get; set; }
        long SearchId { get; set; }
        List<ISelectListItem> DDLSearch { get; set; }
        string TxtSearch { get; set; }  
    }
}