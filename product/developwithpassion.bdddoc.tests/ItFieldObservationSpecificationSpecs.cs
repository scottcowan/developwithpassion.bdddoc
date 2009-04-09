 using System;
 using System.Reflection;
 using developwithpassion.bdd.contexts;
 using developwithpassion.bdd.mbunit.standard.observations;
 using developwithpassion.bdddoc.core;
 using developwithpassion.bdddoc.domain;
 using developwithpassion.bdd.mbunit;
 using developwithpassion.bdddoc.tests.utility;
 using System.Linq;

namespace developwithpassion.bdddoc.tests
 {   
     public class ItFieldObservationSpecificationSpecs
     {
         public abstract class concern : observations_for_a_sut_with_a_contract<IObservationSpecification,
                                            ItFieldObservationSpecification>
         {
        
         }

         [Concern(typeof(ItFieldObservationSpecification))]
         public class when_determining_if_a_delegate_field_meets_the_specification : concern
         {
             context c = () =>
             {
                 member = typeof (when_a_number_is_told_to_subtract_itself_to_another_number).GetMember("should_be_recognized_as_an_observation", BindingFlags.Instance | BindingFlags.NonPublic).First();
                 provide_a_basic_sut_constructor_argument("it");
             };

             because b = () =>
             {
                 result = sut.is_satisfied_by(member);
             };

        
             it should_be_an_observation_if_the_delegate_field_type_matches_the_specified_field_type_for_a_delegate_field_observation = () =>
             {
                 result.should_be_true();
             };

             static bool result;
             static MemberInfo member;
         }
     }
 }
