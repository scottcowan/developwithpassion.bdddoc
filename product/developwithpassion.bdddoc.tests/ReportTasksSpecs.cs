using System;
using System.IO;
using System.Reflection;
using System.Text;
using developwithpassion.bdd;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdddoc.core;
using developwithpassion.bdddoc.domain;
using developwithpassion.bdddoc.tasks;
using Rhino.Mocks;

namespace developwithpassion.bdddoc.tests
{
    public class ReportTaskSpecs
    {
        public abstract class concern : observations_for_a_sut_with_a_contract<IReportTasks, ReportTasks>
        {
            static protected IReportOptions report_options;
            static protected IReportOptionsFactory report_options_factory;
            static protected ITestReportFactory test_report_factory;
            static protected StringBuilder builder;
            static protected IConcernReportFactory concern_report_factory;
            static protected IReportWriter report_writer;
            static protected IConcernReport report;
            static protected IObservationReport observations;
            static protected TextWriter writer;

            context c = () =>
            {
                report_options = new ReportOptions();
                report_writer = the_dependency<IReportWriter>();
                report_options_factory = the_dependency<IReportOptionsFactory>();
                concern_report_factory = the_dependency<IConcernReportFactory>();
                test_report_factory = the_dependency<ITestReportFactory>();
                builder = new StringBuilder();
                writer = new StringWriter(builder);
                provide_a_basic_sut_constructor_argument(writer);
                report = an<IConcernReport>();
                observations = an<IObservationReport>();
            };
        }

        [Concern(typeof (ReportTasks))]
        public class when_told_to_run_the_report_and_the_options_created_from_the_arguments_are_not_valid : concern
        {
            context c = () =>
            {
                report_options = an<IReportOptions>();
                report_options.Stub(x => x.is_valid).Return(false);
                report_options_factory.Stub(x => x.create_from(null)).IgnoreArguments().Return(report_options);
            };

            because b = () =>
            {
                sut.run_report_using(new string[0]);
            };


            it should_write_out_the_help_message_to_the_text_writer = () =>
            {
                builder.ToString().should_be_equal_ignoring_case(ReportTasks.help_message);
            };
        }

        [Concern(typeof (ReportTasks))]
        public class when_told_to_run_the_report_and_the_options_created_from_the_arguments_are_valid : concern
        {
            context c = () =>
            {
                report_options.output_filename = "blah";
                report_options_factory.Stub(x => x.create_from(null)).IgnoreArguments().Return(report_options);
                test_report_factory.Stub(x => x.create_using(report_options)).Return(observations);
                concern_report_factory.Stub(x => x.create_using(report_options, observations)).Return(report);
            };

            because b = () =>
            {
                sut.run_report_using(new string[0]);
            };


            it should_tell_the_report_writer_to_save_the_report = () =>
            {
                report_writer.received(x => x.save(report, "blah"));
            };
        }

        [Concern(typeof (ReportTasks))]
        public class when_generating_a_real_report : concern
        {
            context c = () =>
            {
                report_options_factory.Stub(x => x.create_from(null)).IgnoreArguments().Return(create_report_options());
                concern_report_factory = new ConcernReportFactory();
                report_writer = new SimpleHtmlReportWriter();
                test_report_factory = new MbUnitReportFactory();
                writer = Console.Out;
                File.Delete(report_output_path);
            };


            after_each_observation ae = () =>
            {
                File.Delete(report_output_path);
            };

            static string report_output_path
            {
                get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.html"); }
            }

            static IReportOptions create_report_options()
            {
                return new ReportOptions
                       {
                           assembly_to_scan = Assembly.GetExecutingAssembly(),
                           observation_specification = new ObservationAttributeSpecification("ObservationAttribute"),
                           output_filename = report_output_path
                       };
            }

            because b = () =>
            {
                sut.run_report_using(null);
            };


            it should_generate_the_report_file = () =>
            {
                File.Exists(report_output_path).should_be_true();
            };

            public override IReportTasks create_sut()
            {
                return new ReportTasks(writer, report_options_factory, concern_report_factory, test_report_factory, report_writer);
            }
        }
    }
}