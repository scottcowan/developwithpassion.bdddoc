using System;

namespace developwithpassion.bdddoc.domain
{
    public interface IConcernAttribute
    {
        Type concerned_with { get; }
        string story_key { get; }
    }
}