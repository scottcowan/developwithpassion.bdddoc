using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tests
{
    public class BDDStyleNameSpecs
    {
        public abstract class concern : observations_for_a_sut_without_a_contract<BDDStyleName>
        {
            context c = () =>
            {
                provide_a_basic_sut_constructor_argument(spec_name_with_underscores);
            };

            static protected string spec_name_with_underscores = "name_formatted_with_jp_s_bdd_conventions";
            static protected string name_expanded_using_conventions = "name formatted with jp's bdd conventions";
        }

        [Concern(typeof (BDDStyleName))]
        public class when_a_bdd_style_name_is_asked_for_its_name : concern
        {
            because b = () =>
            {
                name = sut.name;
            };


            it should_return_its_name_using_bdd_style_naming_conventions = () =>
            {
                name.should_be_equal_ignoring_case(name_expanded_using_conventions);
            };

            static string name;
        }

        [Concern(typeof (BDDStyleName))]
        public class when_a_bdd_style_name_is_implicitly_converted_to_a_string : concern
        {
            because b = () =>
            {
                name = sut;
            };


            it should_return_its_name_using_bdd_style_naming_conventions = () =>
            {
                name.should_be_equal_ignoring_case(name_expanded_using_conventions);
            };

            static string name;
        }

        [Concern(typeof (BDDStyleName))]
        public class when_a_bdd_style_name_is_converted_to_a_string : concern
        {
            static string result;


            because b = () =>
            {
                result = sut.ToString();
            };


            it should_return_its_name_using_bdd_style_naming_conventions = () =>
            {
                result.should_be_equal_ignoring_case(name_expanded_using_conventions);
            };

            static string original_name;
        }

        [Concern(typeof (BDDStyleName))]
        public class when_a_bdd_style_name_determines_equality : concern
        {
            context c = () =>
            {
                other_instance_of_same_name = new BDDStyleName(spec_name_with_underscores);
            };


            it should_be_equal_if_the_undelrying_name_is_exactly_the_same = () =>
            {
                sut.Equals(other_instance_of_same_name).should_be_true();
                sut.Equals("Name".as_bdd_style_name()).should_be_false();
                sut.Equals("name 2".as_bdd_style_name()).should_be_false();
            };

            static BDDStyleName other_instance_of_same_name;
        }
    }
}