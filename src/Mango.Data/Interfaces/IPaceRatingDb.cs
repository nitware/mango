using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Mango.Data.Interfaces
{
    public interface IPaceRatingDb
    {
        string FIELD_PACE_RATING_ID { get; }
        string FIELD_RATING { get; }
        string FIELD_FROM { get; }
        string FIELD_TO { get; }
        string FIELD_GRADE { get; }
        string FIELD_DEFINITION { get; }

        DataSet SelectPaceRatingByFromAndTo(decimal totalScore);
        DataSet SelectAllPaceRating();
       
    }


}
