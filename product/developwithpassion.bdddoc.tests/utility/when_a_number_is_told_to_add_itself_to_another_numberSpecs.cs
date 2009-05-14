using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit.standard;
using developwithpassion.bdddoc.core;


namespace developwithpassion.bdddoc.tests.utility
{
    [Concern(typeof (int),"sample story")]
    public class when_a_number_is_told_to_add_itself_to_another_number
    {
        [Observation]
        public void should_add_one_number_to_another()
        {
        }

        [Observation]
        public void should_jump_one_to_another_and_see_what_is_said()
        {
        }

        public void should_find_the_information_from_the_name_of_the_test()
        {
        }
    }

    [Concern(typeof (int))]
    public class when_a_number_is_told_to_subtract_itself_to_another_number
    {
        [Observation]
        public void should_add_one_number_to_another()
        {
        }

        [Observation]
        public void should_jump_one_to_another_and_see_what_is_said()
        {
        }

        public void a_method_that_is_not_an_observation()
        {
        }

        it should_be_recognized_as_an_observation = () => {};


    }

    [Concern(typeof (decimal))]
    public class when_a_decimal_is_told_to_subtract_itself_to_another_number
    {
        [Observation]
        public void should_add_one_number_to_another()
        {
        }

        [Observation]
        public void should_jump_one_to_another_and_see_what_is_said()
        {
        }

        public void should_find_the_information_from_the_name_of_the_test()
        {
        }
    }

    public class something_that_is_not_a_concern
    {
    }
}