using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tests
{
    public class ObservationReportSpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<ObservationReport> {}

        public class when_searching_the_observation_report : concern
        {
            context c = () =>
            {
                var list = new List<IObservation> {new ObservationResult {name = "test", success = true}};
                provide_a_basic_sut_constructor_argument<IEnumerable<IObservation>>(list);
            };

            because b = () =>
            {
                result = sut.from_full_name(full_name);
            };

            it should_find_a_known_observations = () =>
            {
                result.should_not_be_null();
                result.success.should_be_true();
            };

            static string full_name = "test";
            static IObservationReport report;
            static IObservation result;
        }
    }
}