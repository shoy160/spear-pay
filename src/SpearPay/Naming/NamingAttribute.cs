using System;

namespace SpearPay.Naming
{

    public class IgnoreAttribute : Attribute { }
    public class NamingAttribute : Attribute
    {
        public string Name { get; set; }

        public NamingCase NamingCase { get; set; } = NamingCase.None;

        public NamingAttribute(string name)
        {
            Name = name;
        }
    }
}
