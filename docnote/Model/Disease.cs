using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    public class Disease
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            Disease personObj = obj as Disease;
            if (personObj == null)
                return false;
            else
                return Code.Equals(personObj.Code);
        }

        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }
    }
}
