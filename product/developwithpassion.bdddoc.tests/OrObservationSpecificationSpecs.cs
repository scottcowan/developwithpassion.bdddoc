 using System;
 using System.Reflection;
 using developwithpassion.bdd.contexts;
 using developwithpassion.bdd.mbunit.standard.observations;
 using developwithpassion.bdddoc.core;
 using developwithpassion.bdddoc.domain;
 using developwithpassion.bdd.mbunit;
 using developwithpassion.bdddoc.tests.utility;
 using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
 {   
     public class OrObservationSpecificationSpecs
     {
         public abstract class concern : observations_for_a_sut_with_a_contract<IObservationSpecification,
                                             OrObservationSpecification>
         {
             static IObservationSpecification first_specification;
             static IObservationSpecification second_specification;

             context c = () =>
             {
                 first_specification = an<IObservationSpecification>();
                 second_specification = an<IObservationSpecification>();

                 first_specification.Stub(x => x.is_satisfied_by(Arg<MemberInfo>.Is.Anything)).Return(true);
             };

             public override IObservationSpecification create_sut()
             {
                 return new OrObservationSpecification(first_specification, second_specification);
             }
         }

         [Concern(typeof(OrObservationSpecification))]
         public class when_determining_if_it_is_satisfied_by_a_member_info : concern
         {
             context c = () =>
             {
               member =Method.pointed_at_by(new when_a_number_is_told_to_subtract_itself_to_another_number().should_add_one_number_to_another);
             };

             because b = () =>
             {
                 result =sut.is_satisfied_by(member);
             };

        
             it should_be_satisfied_if_either_one_of_the_specifications_it_is_composed_with_is_satisfied = () =>
             {
                 result.should_be_true();
             };

             static bool result;
             static MemberInfo member;
         }
     }
 }
