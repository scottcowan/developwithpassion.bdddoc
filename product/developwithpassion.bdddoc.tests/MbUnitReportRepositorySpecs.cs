using System;
using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tests
{
    public class MbUnitReportRepositorySpecs
    {
        public class concern : observations_for_a_sut_without_a_contract<MbUnitReportRepository> {}

        public class when_using_a_mbunit_test_repository : concern
        {
            because b = () =>
            {
                result = sut.all_observations(@"C:\development\course\store\trunk\build\buildartifacts\mbunit\test_report.xml");
            };

            it should_find_observations_in_a_report = () =>
            {
                var list = new List<IObservation>(result);
                list.Count.should_be_greater_than(0);
                foreach (var test_result in list)
                {
                    Console.WriteLine("{0} {1}", test_result.name, test_result.success);
                }
            };

            static IEnumerable<IObservation> result;
        }
    }
}