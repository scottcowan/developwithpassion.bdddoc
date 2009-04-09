using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
{
    public class ConcernGroupSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IConcernGroup, ConcernGroup>
        {
            context c = () =>
            {
                concern_in_group = an<IConcern>();
                concerns_in_group = new List<IConcern>();
                provide_a_basic_sut_constructor_argument<IEnumerable<IConcern>>(concerns_in_group);
            };

            static protected IConcern concern_in_group;
            static protected IList<IConcern> concerns_in_group;
        }

        [Concern(typeof (ConcernGroup))]
        public class when_a_concern_group_is_created_with_a_list_of_concerns : concern
        {
            context c = () =>
            {
                concerns_in_group.Add(concern_in_group);
            };

            it should_be_able_to_access_the_concerns_later = () =>
            {
                sut.concerns.should_contain(concern_in_group);
            };
        }

        [Concern(typeof (ConcernGroup))]
        public class when_a_concern_group_is_asked_for_its_concern_type : concern
        {
            context c = () =>
            {
                concern_in_group.Stub(x => x.concerned_with).Return(typeof (int));
                concerns_in_group.Add(concern_in_group);
            };

            because b = () =>
            {
                concerned_with = sut.concerned_with;
            };


            it should_return_the_concern_type_of_its_first_concern = () =>
            {
                concerned_with.should_be_equal_to(typeof (int));
            };

            static Type concerned_with;
        }

        [Concern(typeof (ConcernGroup))]
        public class when_a_concern_group_is_asked_for_the_total_number_of_its_concerns : concern
        {
            context c = () =>
            {
                Enumerable.Repeat(an<IConcern>(), 3).each(concerns_in_group.Add);
            };

            because b = () =>
            {
                total_number_of_concerns = sut.total_number_of_concerns;
            };


            it should_sum_up_the_number_of_concerns_it_contains = () =>
            {
                total_number_of_concerns.should_be_equal_to(3);
            };

            static int total_number_of_concerns;
        }

        [Concern(typeof (ConcernGroup))]
        public class when_a_concern_group_is_asked_for_its_total_number_of_observations : concern
        {
            context c = () =>
            {
                concern_in_group.Stub(x => x.total_number_of_observations).Return(1);

                var another_concern_in_group = an<IConcern>();
                another_concern_in_group.Stub(x => x.total_number_of_observations).Return(3);

                concerns_in_group.Add(concern_in_group);
                concerns_in_group.Add(another_concern_in_group);
                concerns_in_group.Add(an<IConcern>());
            };

            because b = () =>
            {
                total_number_of_observations = sut.total_number_of_observations;
            };


            it should_sum_up_number_of_observations_in_each_of_its_concerns = () =>
            {
                total_number_of_observations.should_be_equal_to(4);
            };

            static int total_number_of_observations;
        }
    }
}