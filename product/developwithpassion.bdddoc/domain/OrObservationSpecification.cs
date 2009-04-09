using System;
using System.Reflection;

namespace developwithpassion.bdddoc.domain
{
    public class OrObservationSpecification : IObservationSpecification
    {
        IObservationSpecification left_side;
        IObservationSpecification right_side;

        public OrObservationSpecification(IObservationSpecification left_side, IObservationSpecification right_side)
        {
            this.left_side = left_side;
            this.right_side = right_side;
        }

        public bool is_satisfied_by(MemberInfo member)
        {
            return left_side.is_satisfied_by(member) || right_side.is_satisfied_by(member);
        }
    }
}