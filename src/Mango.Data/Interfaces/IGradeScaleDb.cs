using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Mango.Data.Interfaces
{
    public interface IGradeScaleDb
    {
        string FIELD_GRADE_SCALE_ID { get; }
        string FIELD_FROM { get; }
        string FIELD_TO { get; }

        DataSet SelectGradeScaleByFromAndTo(decimal totalScore);
        DataSet SelectAllGradeScale();
       
    }


}
