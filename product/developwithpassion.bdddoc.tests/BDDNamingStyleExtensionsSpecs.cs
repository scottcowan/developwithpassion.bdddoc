using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tests
{
    public class BDDNamingStyleExtensionsSpecs
    {
        public abstract class concern : observations_for_a_static_sut {}

        [Concern(typeof (BDDNamingStyleExtensions))]
        public class when_creating_a_bdd_style_name_from_a_string : concern
        {
            static BDDStyleName result;


            because b = () =>
            {
                result = "when_creating_a_bdd_name_from_a_string".as_bdd_style_name();
            };

            it should_create_a_bdd_style_name_from_the_original_string = () =>
            {
                result.name.should_be_equal_ignoring_case("when creating a bdd name from a string");
            };
        }
    }
}