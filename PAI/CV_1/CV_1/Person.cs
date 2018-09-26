using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CV_1
{
    [DataContract]
    class Person
    {
        [DataMember]
        public string GivenName { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Roles { get; set; }
        [DataMember]
        public string FamilyName { get; set; }

        public override string ToString()
        {
            return("GivenName: " + GivenName + " |Id: " + Id + " |Roles: " + Roles + " |FamilyName: " + FamilyName);
        }
    }
}
