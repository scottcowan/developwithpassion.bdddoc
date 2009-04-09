using System;

namespace developwithpassion.bdddoc.domain
{
    public interface IReportOptionsFactory
    {
        IReportOptions create_from(string[] args);
    }

    public class ReportOptionsFactory : IReportOptionsFactory
    {
        private readonly IAssemblyRepository assembly_resolver;

        public ReportOptionsFactory() : this(new AssemblyRepository())
        {
        }

        public ReportOptionsFactory(IAssemblyRepository assembly_resolver)
        {
            this.assembly_resolver = assembly_resolver;
        }

        public IReportOptions create_from(string[] args)
        {
            try
            {
                return new ReportOptions
                           {
                               assembly_to_scan = assembly_resolver.find_using(args[0]),
                               observation_specification = new ObservationAttributeSpecification(args[1]),
                               output_filename = args[2],
                               mbunit_test_report = args[3]
                           };
            }
            catch (Exception)
            {
                return new MissingReportOptions();
            }
        }
    }
}
