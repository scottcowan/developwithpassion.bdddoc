using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tests
{
    public class ConcernObservationFactorySpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<ConcernObservation> {}

        [Concern(typeof (ConcernObservation))]
        public class when_a_concern_observation_is_asked_for_its_name : concern
        {
            context c = () =>
            {
                provide_a_basic_sut_constructor_argument(new BDDStyleName("what_is_in_a_name"));
                provide_a_basic_sut_constructor_argument(true);
            };

            because b = () =>
            {
                name = sut.name;
            };


            it should_return_the_value_of_its_bdd_name = () =>
            {
                name.should_be_equal_ignoring_case(sut.name);
            };


            static string name;
        }
    }
}