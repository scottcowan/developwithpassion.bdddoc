using System.Reflection;

namespace developwithpassion.bdddoc.domain
{
    public interface IObservationSpecification
    {
        bool IsSatisfiedBy(MemberInfo member);
    }
}