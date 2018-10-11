using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Mango.Model
{
    [DataContract]
    public class Role : SetupBase
    {
        private bool hasUser;

        [DataMember]
        public UserRight UserRight { get; set; }
        [DataMember]
        public List<Right> Rights { get; set; }
        [DataMember]
        public bool HasUser
        {
            get { return hasUser; }
            set
            {
                hasUser = value;
                base.OnPropertyChanged("HasUser");
            }
        }
    }


}
