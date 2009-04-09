using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using developwithpassion.bdddoc.tests.utility;
using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
{
    public class ConcernFactorySpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IConcernFactory, ConcernFactory> {}

        [Concern(typeof (ConcernFactory))]
        public class when_a_concern_factory_is_told_to_create_a_concern_from_a_type : concern
        {
            static IConcern result;
            static IObservationReport observations;
            static IObservationSpecification observation_specification;

            context c = () =>
            {
                observation_specification = an<IObservationSpecification>();
                observations = new ObservationReport(new List<IObservation>());
                observation_specification.Stub(x => x.is_satisfied_by(null)).IgnoreArguments().Return(true);
            };


            because b = () =>
            {
                result = sut.create_concern_from(typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number), observation_specification, observations);
            };


            it should_create_a_concern_with_the_correct_bdd_style_name = () =>
            {
                result.name.should_be_equal_to(typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number).Name.as_bdd_style_name());
            };

            it should_have_the_concern_populated_with_all_of_the_observations_satisfied_by_the_specification = () =>
            {
                result.total_number_of_observations.should_be_equal_to(4);
            };
        }
    }
}