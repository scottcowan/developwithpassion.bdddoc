using System;

namespace developwithpassion.bdddoc.core
{
    public interface IConcernAttribute
    {
        Type concerned_with { get; }
        string story_key { get; }
    }
}