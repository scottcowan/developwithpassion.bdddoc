using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
{
    public class StoryRepositorySpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IStoryRepository, StoryRepository>
        {
            context c = () =>
            {
                concern_factory = the_dependency<IConcernFactory>();
                story_factory = the_dependency<IStoryFactory>();
                options = an<IReportOptions>();
                observation_specification = an<IObservationSpecification>();
                specific_concern = new Concern(typeof (int), "", null, null);
                specific_concern_group = new ConcernGroup(new[] {specific_concern});
                specific_story = new StoryReport(new[] {specific_concern_group});
                observations = an<IObservationReport>();
            };

            static protected IEnumerable<IStoryReport> all_stories;
            static protected IObservationReport observations;
            static protected IConcern specific_concern;
            static protected IConcernGroup specific_concern_group;
            protected static IStoryReport specific_story;
            static protected IObservationSpecification observation_specification;
            static protected IConcernFactory concern_factory;
            static protected IStoryFactory story_factory;
            static protected IReportOptions options;
        }

        [Concern(typeof (StoryRepository))]
        public class when_a_story_repository_is_told_to_find_all_of_The_concern_groups : concern
        {
            context c = () =>
            {
                options.assembly_to_scan = Assembly.GetExecutingAssembly();
                //specific_concern.Stub(x => x.concerned_with).Return(typeof (int));
                //specific_concern.Stub(x => x.story_key).Return("story");
                concern_factory.Stub(x => x.create_concern_from(null, observation_specification, observations)).IgnoreArguments().Return(specific_concern);
                story_factory.Stub(x => x.create_from_concerns(null)).IgnoreArguments().Return(specific_story);
            };

            because b = () =>
            {
                all_stories = sut.all_concern_groups_found_using(options, observations);
            };


            it should_find_all_of_the_groups_in_the_assembly_using_the_options_it_was_created_with = () =>
            {
                all_stories.Sum(x => x.groups.Sum(y=>y.total_number_of_concerns)).should_be_greater_than(0);
            };
        }
    }
}