using System;

namespace developwithpassion.bdddoc.core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConcernAttribute : Attribute
    {
        public string story_key { get; private set; }
        public Type concerned_with { get; private set; }

        public ConcernAttribute(Type concern) : this(concern,"") { }

        public ConcernAttribute(Type concern, string story_key)
        {
            this.concerned_with = concern;
            this.story_key = story_key;
        }
    }
}