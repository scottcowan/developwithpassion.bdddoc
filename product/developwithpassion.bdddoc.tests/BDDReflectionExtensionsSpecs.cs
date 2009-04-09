using System;
using System.Collections.Generic;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using developwithpassion.bdddoc.tests.utility;

namespace developwithpassion.bdddoc.tests
{
    public class BDDReflectionExtensionsSpecs
    {
        public abstract class concern : observations_for_a_static_sut {}

        [Concern(typeof (BDDReflectionExtensions))]
        public class when_determining_whether_a_type_is_a_concern : concern
        {
            context c = () =>
            {
                type_that_is_a_concern = typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number);
                type_that_is_not_a_concern = typeof (something_that_is_not_a_concern);
            };


            it should_only_be_a_concern_if_it_has_the_concern_attribute = () =>
            {
                type_that_is_a_concern.is_a_concern().should_be_true();
                type_that_is_not_a_concern.is_a_concern().should_be_false();
            };

            static Type type_that_is_a_concern;
            static Type type_that_is_not_a_concern;
        }

        [Concern(typeof (BDDReflectionExtensions))]
        public class when_building_a_concern_observation_from_a_method_that_is_an_observation : concern
        {
            context c = () =>
            {
                var type_with_observations = new when_a_decimal_is_told_to_subtract_itself_to_another_number();
                observation = Method.pointed_at_by(type_with_observations.should_jump_one_to_another_and_see_what_is_said);
                observations = new ObservationReport(new List<IObservation>());
            };

            because b = () =>
            {
                result = observation.as_observation("", observations);
            };

            it should_create_a_concern_observation_that_has_the_bdd_style_name = () =>
            {
                result.should_not_be_null();
                result.name.should_be_equal_to(new BDDStyleName(observation.Name));
            };

            static MemberInfo observation;
            static IConcernObservation result;
            static IObservationReport observations;
        }

        [Concern(typeof (BDDReflectionExtensions))]
        public class when_finding_all_concerns_in_a_specific_assembly : concern
        {
            because b = () =>
            {
                concern_types = Assembly.GetExecutingAssembly().all_types_with_a_concern_attribute();
            };


            it should_return_all_types_that_have_the_concern_attribute_applied_to_them = () =>
            {
                concern_types.should_contain(typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number),
                                             typeof (when_a_number_is_told_to_add_itself_to_another_number),
                                             typeof (when_a_number_is_told_to_subtract_itself_to_another_number));
            };

            it should_not_return_types_that_do_not_have_the_concern_attribute_on_them = () =>
            {
                concern_types.should_not_contain(typeof (something_that_is_not_a_concern));
            };

            static IEnumerable<Type> concern_types;
        }
    }
}