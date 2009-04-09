using System.Collections.Generic;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.utility;

namespace developwithpassion.bdddoc.tests
{
    public class EnumerableExtensionsSpecs
    {
        public abstract class concern : observations_for_a_static_sut {}

        [Concern(typeof (EnumerableExtensions))]
        public class when_an_enumerable_is_walked_one_item_at_a_time : concern
        {
            because b = () =>
            {
                all_items_one_at_a_time = new List<int> {1, 2, 3, 4}.one_at_a_time();
            };


            it should_get_an_set_of_items_that_contains_each_of_the_original_items = () =>
            {
                all_items_one_at_a_time.should_only_contain(1, 2, 3, 4);
            };

            static IEnumerable<int> all_items_one_at_a_time;
        }

        [Concern(typeof (EnumerableExtensions))]
        public class when_each_is_invoked : concern
        {
            static IList<int> all_items;

            context c = () =>
            {
                all_items = new List<int>();
            };

            because b = () =>
            {
                new List<int> {1, 2, 3, 4}.each(x => all_items.Add(x));
            };


            it should_perform_the_action_for_each_item_in_the_enumerable = () =>
            {
                all_items.should_only_contain(1, 2, 3, 4);
            };
        }
    }
}