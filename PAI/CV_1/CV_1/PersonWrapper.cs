using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CV_1
{
    [DataContract]
    class PersonWrapper
    {
        [DataMember]
        public Person Person { get; set; }

        public override string ToString()
        {
            return Person.ToString();
        }
    }
}
