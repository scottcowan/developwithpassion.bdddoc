using System.Reflection;

namespace developwithpassion.bdddoc.domain
{
    public class ItFieldObservationSpecification : IObservationSpecification
    {
        public ItFieldObservationSpecification()
        {
        }

        public bool is_satisfied_by(MemberInfo member)
        {
            return member.MemberType == MemberTypes.Field &&
                   ((FieldInfo) member).FieldType.Name == "it";
        }
    }
}