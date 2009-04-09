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
    public class ConcernGroupRepositorySpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IConcernGroupRepository, ConcernGroupRepository>
        {
            context c = () =>
            {
                concern_factory = the_dependency<IConcernFactory>();
                options = an<IReportOptions>();
                observation_specification = an<IObservationSpecification>();
                specific_concern = an<IConcern>();
                observations = an<IObservationReport>();
            };

            static protected IEnumerable<IConcernGroup> all_groups;
            static protected IObservationReport observations;
            static protected IConcern specific_concern;
            static protected IObservationSpecification observation_specification;
            static protected IConcernFactory concern_factory;
            static protected IReportOptions options;
        }

        [Concern(typeof (ConcernGroupRepository))]
        public class when_a_concern_group_repository_is_told_to_find_all_of_The_concern_groups : concern
        {
            context c = () =>
            {
                options.assembly_to_scan = Assembly.GetExecutingAssembly();
                specific_concern.Stub(x => x.concerned_with).Return(typeof (int));
                concern_factory.Stub(x => x.create_concern_from(null, observation_specification, observations)).IgnoreArguments().Return(specific_concern);
            };

            because b = () =>
            {
                all_groups = sut.all_concern_groups_found_using(options, observations);
            };


            it should_find_all_of_the_groups_in_the_assembly_using_the_options_it_was_created_with = () =>
            {
                all_groups.Where(x => x.concerned_with.Equals(typeof (int))).Count().should_be_greater_than(0);
            };
        }
    }
}