using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mango.Data;
using Mango.Model.Model;
using Mango.Model.Translator;

namespace Mango.Business
{
    public class StatusLogic : BusinessLogicBase<Status, STATUS>
    {
        public StatusLogic()
        {
            base.translator = new StatusTranslator();
        }
    }


}
