using System;
using System.Collections.Generic;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
{
    public class ReportOptionsFactorySpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IReportOptionsFactory, ReportOptionsFactory>
        {
            static protected IAssemblyRepository assembly_resolver;
            static protected string[] args;
            static protected string assembly_name;

            context c = () =>
            {
                assembly_resolver = the_dependency<IAssemblyRepository>();
                assembly_name = "MbUnit.Framework.dll";
                args = new List<string> {assembly_name, "ObservationAttribute", "output.html","test-result.xml"}.ToArray();
            };
        }

        [Concern(typeof (ReportOptionsFactory))]
        public class when_creating_report_options_from_an_array_of_valid_arguments : concern
        {
            context c = () =>
            {
                assembly_resolver.Stub(x => x.find_using(assembly_name)).Return(Assembly.GetExecutingAssembly());
            };

            because b = () =>
            {
                result = sut.create_from(args);
            };

            it should_return_valid_report_options = () =>
            {
                result.assembly_to_scan.should_be_equal_to(Assembly.GetExecutingAssembly());
                result.observation_specification.should_not_be_null();
            };

            static IReportOptions result;
        }

        [Concern(typeof (ReportOptionsFactory))]
        public class when_an_exception_is_thrown_while_trying_to_to_create_the_report_options : concern
        {
            context c = () =>
            {
                assembly_resolver.Stub(x => x.find_using(null)).IgnoreArguments().Throw(new Exception());
            };

            because b = () =>
            {
                result = sut.create_from(args);
            };

            it should_return_the_missing_options_null_object = () =>
            {
                result.should_be_an_instance_of<MissingReportOptions>();
            };

            static IReportOptions result;
        }
    }
}