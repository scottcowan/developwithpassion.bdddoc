using System;

namespace developwithpassion.bdddoc.core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConcernAttribute : Attribute
    {
        public Type concerned_with { get; private set; }

        public ConcernAttribute(Type concern)
        {
            this.concerned_with = concern;
        }
    }
}