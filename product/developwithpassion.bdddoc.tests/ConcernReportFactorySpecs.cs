using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
{
    public class ConcernReportFactorySpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IConcernReportFactory, ConcernReportFactory>
        {
            context c = () =>
            {
                concern_group_repository = the_dependency<IConcernGroupRepository>();
                observations = an<IObservationReport>();
                concern_groups = new List<IConcernGroup>();
                options = an<IReportOptions>();
            };

            static protected List<IConcernGroup> concern_groups;
            static protected IConcernGroupRepository concern_group_repository;
            static protected IConcernReport report;
            static protected IReportOptions options;
            static protected IObservationReport observations;
        }

        [Concern(typeof (ConcernReportFactory))]
        public class when_the_concern_report_factory_is_told_to_create_a_concern_report : concern
        {
            context c = () =>
            {
                concern_groups.Add(an<IConcernGroup>());
                concern_groups.Add(an<IConcernGroup>());
                concern_group_repository.Stub(x => x.all_concern_groups_found_using(options, observations)).Return(concern_groups);
            };

            because b = () =>
            {
                report = sut.create_using(options, observations);
            };


            it should_create_a_report_that_consists_of_all_the_concern_groups_found_by_the_repository = () =>
            {
                report.groups.Count().should_be_equal_to(2);
            };
        }
    }
}