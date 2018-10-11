using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.DataAccess;
using System.Data;

namespace Mango.Data.Interfaces
{
    public interface IOptionDb
    {
        string FIELD_OPTION_ID { get; }
        string FIELD_OPTION_NAME { get; }
             
        bool InsertOption(byte optionId, string optionName, Transaction transaction);
        bool DeleteOptionByOptionId(byte optionId, Transaction transaction);
        bool UpdateOptionByOptionId(byte optionId, string optionName, Transaction transaction);
        DataSet SelectAllOption();
        DataSet SelectOptionByOptionId(byte optionId);
       
    }



}
