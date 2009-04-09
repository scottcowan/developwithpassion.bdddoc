 using System.Reflection;
 using developwithpassion.bdd.contexts;
 using developwithpassion.bdd.mbunit.standard.observations;
 using developwithpassion.bdddoc.core;
 using developwithpassion.bdddoc.domain;
 using developwithpassion.bdddoc.tests.utility;
 using developwithpassion.bdd.mbunit;
 using System.Linq;

namespace developwithpassion.bdddoc.tests
 {   
     public class ObservationSpecificationFactorySpecs
     {
         public abstract class concern : observations_for_a_sut_with_a_contract<IObservationSpecificationFactory,
                                             ObservationSpecificationFactory>
         {
        
         }

         [Concern(typeof(ObservationSpecificationFactory))]
         public class when_creating_an_observation_specification_from_an_observation_attribute_name : concern
         {
             because b = () =>
             {
                 result = sut.create_from("ObservationAttribute");
             };

        
             it should_return_a_composed_specification_that_handles_it_blocks_and_methods_with_matching_observation_attributes = () =>
             {
                 result.is_satisfied_by(Method.pointed_at_by(new when_a_number_is_told_to_subtract_itself_to_another_number().should_add_one_number_to_another)).should_be_true();
                 result.is_satisfied_by(typeof(when_a_number_is_told_to_subtract_itself_to_another_number).GetMember("should_be_recognized_as_an_observation",BindingFlags.Instance | BindingFlags.NonPublic).First()).should_be_true();
             };

             static IObservationSpecification result;
         }
     }
 }
