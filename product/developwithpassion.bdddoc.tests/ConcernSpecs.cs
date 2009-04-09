using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tests
{
    public class ConcernSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IConcern, Concern>
        {
            context c = () =>
            {
                observations = new List<IConcernObservation>();
                observation = an<IConcernObservation>();

                provide_a_basic_sut_constructor_argument(typeof (int));
                provide_a_basic_sut_constructor_argument(new BDDStyleName("when_looking_for_a_certain_number"));
                provide_a_basic_sut_constructor_argument<IEnumerable<IConcernObservation>>(observations);
            };

            static protected List<IConcernObservation> observations;
            static protected IConcernObservation observation;
            static protected string name_of_concern;
        }

        [Concern(typeof (Concern))]
        public class when_a_concern_is_asked_for_its_name : concern
        {
            because b = () =>
            {
                name_of_concern = sut.name;
            };


            it should_return_the_value_of_its_bdd_name = () =>
            {
                name_of_concern.should_be_equal_ignoring_case(sut.name);
            };
        }


        [Concern(typeof (Concern))]
        public class when_calculating_the_total_number_of_observations : concern
        {
            context c = () =>
            {
                observations.Add(an<IConcernObservation>());
                observations.Add(an<IConcernObservation>());
            };

            because b = () =>
            {
                total_number_of_observations = sut.total_number_of_observations;
            };

            it should_sum_the_number_of_observations_it_contains = () =>
            {
                total_number_of_observations.should_be_equal_to(2);
            };

            static int total_number_of_observations;
        }
    }
}