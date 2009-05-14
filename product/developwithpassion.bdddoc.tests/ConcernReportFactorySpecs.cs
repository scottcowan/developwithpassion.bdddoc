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
                StoryRepository = the_dependency<IStoryRepository>();
                observations = an<IObservationReport>();
                stories = new List<IStoryReport>();
                options = an<IReportOptions>();
            };

            static protected List<IStoryReport> stories;
            static protected IStoryRepository StoryRepository;
            static protected IConcernReport report;
            static protected IReportOptions options;
            static protected IObservationReport observations;
        }

        [Concern(typeof (ConcernReportFactory))]
        public class when_the_concern_report_factory_is_told_to_create_a_concern_report : concern
        {
            context c = () =>
            {
                var story = new StoryReport(new[]
                                                {
                                                    an<IConcernGroup>(),
                                                    an<IConcernGroup>()
                                                });
                stories.Add(story);
                StoryRepository.Stub(x => x.all_concern_groups_found_using(options, observations)).Return(stories);
            };

            because b = () =>
            {
                report = sut.create_using(options, observations);
            };


            it should_create_a_report_that_consists_of_all_the_concern_groups_found_by_the_repository = () =>
            {
                report.total_number_of_groups.should_be_equal_to(2);
            };
        }
    }
}