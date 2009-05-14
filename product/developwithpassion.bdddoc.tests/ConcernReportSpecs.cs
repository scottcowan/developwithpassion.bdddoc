using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
{
    public class ConcernReportSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IConcernReport, ConcernReport>
        {
            static protected List<IStoryReport> concern_groups_in_report;

            context c = () =>
            {
                concern_groups_in_report = new List<IStoryReport>();
                provide_a_basic_sut_constructor_argument<IEnumerable<IStoryReport>>(concern_groups_in_report);
            };
        }

        [Concern(typeof (ConcernReport))]
        public class when_a_concern_report_with_concern_groups_is_asked_for_the_number_of_concerns_it_has : concern
        {
            context c = () =>
            {
                first_concern_group = an<IStoryReport>();
                second_concern_group = an<IStoryReport>();

                first_concern_group.Stub(x => x.total_number_of_concerns).Return(15);
                second_concern_group.Stub(x => x.total_number_of_concerns).Return(5);
                concern_groups_in_report.Add(first_concern_group);
                concern_groups_in_report.Add(second_concern_group);
            };

            because b = () =>
            {
                total_number_of_concerns = sut.total_number_of_concerns;
            };


            it should_sum_up_the_number_of_concerns_in_all_of_its_concern_groups = () =>
            {
                total_number_of_concerns.should_be_equal_to(20);
            };

            static IStoryReport first_concern_group;
            static int total_number_of_concerns;
            static IStoryReport second_concern_group;
        }

        [Concern(typeof (ConcernReport))]
        public class when_a_concern_report_with_concern_groups_is_asked_for_the_number_of_observations_it_has : concern
        {
            context c = () =>
            {
                first_concern_group = an<IStoryReport>();
                second_concern_group = an<IStoryReport>();

                

                first_concern_group.Stub(x => x.total_number_of_observations).Return(15);
                second_concern_group.Stub(x => x.total_number_of_observations).Return(20);
                concern_groups_in_report.Add(first_concern_group);
                concern_groups_in_report.Add(second_concern_group);
            };

            because b = () =>
            {
                total_number_of_observations = sut.total_number_of_observations;
            };


            it should_sum_up_the_number_of_observations_in_all_of_its_concern_groups = () =>
            {
                total_number_of_observations.should_be_equal_to(35);
            };

            static IStoryReport first_concern_group;
            static int total_number_of_observations;
            static IStoryReport second_concern_group;
        }
    }
}