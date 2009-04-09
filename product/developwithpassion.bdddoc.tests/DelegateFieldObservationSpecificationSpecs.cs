 using developwithpassion.bdd.contexts;
 using developwithpassion.bdd.mbunit.standard.observations;
 using developwithpassion.bdddoc.core;
 using developwithpassion.bdddoc.domain;
 using developwithpassion.bdd.mbunit;
 using developwithpassion.bdddoc.tests.utility;

namespace developwithpassion.bdddoc.tests
 {   
     public class DelegateFieldObservationSpecificationSpecs
     {
         public abstract class concern : observations_for_a_sut_with_a_contract<IObservationSpecification,
                                            DelegateFieldObservationSpecification>
         {
        
         }

         [Concern(typeof(DelegateFieldObservationSpecification))]
         public class when_determining_if_a_delegate_field_meets_the_specification : concern
         {
             context c = () =>
             {
            
             };

             because b = () =>
             {
                sut.IsSatisfiedBy(Method.pointed_at_by()) 
             };

        
             it should_be_an_observation_if_the_delegate_field_type_matches_the_specified_field_type_for_a_delegate_field_observation = () =>
             {
                 result.should_be_true();
             };

             static bool result;
         }
     }
 }
