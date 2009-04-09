using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using developwithpassion.bdddoc.tests.utility;

namespace developwithpassion.bdddoc.tests
{
    public class ObservationAttributeSpecificationSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IObservationSpecification, ObservationAttributeSpecification>
        {
            context c = () =>
            {
                provide_a_basic_sut_constructor_argument("ObservationAttribute");
            };
        }

        [Concern(typeof (ObservationAttributeSpecification))]
        public class when_determining_if_a_method_is_an_observation : concern
        {
            static MemberInfo a_method_that_is_an_observation;
            static MemberInfo a_method_that_is_not_an_observation;

            context c = () =>
            {
                var a_concern_with_observations = new when_a_number_is_told_to_subtract_itself_to_another_number();
                a_method_that_is_an_observation = Method.pointed_at_by(a_concern_with_observations.should_add_one_number_to_another);
                a_method_that_is_not_an_observation = Method.pointed_at_by(a_concern_with_observations.a_method_that_is_not_an_observation);
            };

            it should_be_an_observation_if_it_has_the_specified_named_observation_attribute = () =>
            {
                sut.IsSatisfiedBy(a_method_that_is_an_observation).should_be_true();
                sut.IsSatisfiedBy(a_method_that_is_not_an_observation).should_be_false();
            };
        }
    }
}