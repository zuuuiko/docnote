using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace docnote.Model
{
    public class Disease
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
        [StringLength(20)]
        public string Code { get; set; }
        [StringLength(100)]
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
