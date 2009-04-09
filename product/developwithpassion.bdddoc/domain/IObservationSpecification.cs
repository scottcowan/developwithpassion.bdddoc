using System.Reflection;

namespace developwithpassion.bdddoc.domain
{
    public interface IObservationSpecification
    {
        bool is_satisfied_by(MemberInfo member);
    }
}