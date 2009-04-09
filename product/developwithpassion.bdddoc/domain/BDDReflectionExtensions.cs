using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using developwithpassion.bdddoc.core;

namespace developwithpassion.bdddoc.domain
{
    static public class BDDReflectionExtensions
    {
        static public IEnumerable<Type> all_types_with_a_concern_attribute(this Assembly assembly)
        {
            return assembly.GetTypes().Where(is_a_concern);
        }

        static public bool is_a_concern(this Type type)
        {
            var concern_attributes = type.GetCustomAttributes(typeof (ConcernAttribute), false);
            return concern_attributes != null && concern_attributes.Length == 1;
        }

        static public Type concern(this Type type)
        {
            var concern_attributes = type.GetCustomAttributes(typeof (ConcernAttribute), false);
            return ((ConcernAttribute) concern_attributes[0]).concerned_with;
        }

        static public IConcernObservation as_observation(this MemberInfo method, string concern, IObservationReport observations)
        {
            return new ConcernObservation(method.Name.as_bdd_style_name(), observations.from_full_name(concern + "." + method.Name).success);
        }

        static public IEnumerable<MemberInfo> all_members_that_meet(this Type type, IObservationSpecification observation_specification)
        {
            var member_flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return type.GetMembers(member_flags).Where(observation_specification.is_satisfied_by);
        }

        static public IEnumerable<IConcernObservation> as_observations(this IEnumerable<MemberInfo> methods, string concern, IObservationReport observations)
        {
            return methods.Select(x => x.as_observation(concern, observations));
        }
    }
}