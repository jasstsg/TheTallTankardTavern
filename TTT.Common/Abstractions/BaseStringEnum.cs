using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TTT.Common.Abstractions
{
    public class BaseStringEnum
    {
        private string innerString;

        protected BaseStringEnum(string stringValue)
        {
            this.innerString = stringValue;
        }

        public static implicit operator string(BaseStringEnum bse)
        {
            return bse.innerString;
        }

        public static implicit operator BaseStringEnum(string str)
        {
            return str;
        }

        public override string ToString()
        {
            return this.innerString;
        }
    }
}
