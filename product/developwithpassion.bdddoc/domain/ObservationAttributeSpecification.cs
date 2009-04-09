using System;
using System.Collections.Generic;
using System.Reflection;

namespace developwithpassion.bdddoc.domain
{
    public class ObservationAttributeSpecification : IObservationSpecification
    {
        private readonly string observation_attribute_name;

        public ObservationAttributeSpecification(string observation_attribute_name)
        {
            this.observation_attribute_name = observation_attribute_name;
        }

        public bool is_satisfied_by(MemberInfo member)
        {
            return new List<object>(member.GetCustomAttributes(false))
                .Exists(attribute => is_an_observation_attribute(attribute.GetType()));
        }

        private bool is_an_observation_attribute(Type attribute_type)
        {
            return string.Compare(observation_attribute_name, attribute_type.Name, true) == 0;
        }
    }
}