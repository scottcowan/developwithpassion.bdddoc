using System;

namespace developwithpassion.bdddoc.domain
{
    public interface IReportOptionsFactory
    {
        IReportOptions create_from(string[] args);
    }

    public class ReportOptionsFactory : IReportOptionsFactory
    {
        private IAssemblyRepository assembly_resolver;
        IObservationSpecificationFactory observation_specification_factory;


        public ReportOptionsFactory() : this(new AssemblyRepository(),new ObservationSpecificationFactory())
        {
        }

        public ReportOptionsFactory(IAssemblyRepository assembly_resolver, IObservationSpecificationFactory observation_specification_factory)
        {
            this.assembly_resolver = assembly_resolver;
            this.observation_specification_factory = observation_specification_factory;
        }

        public IReportOptions create_from(string[] args)
        {
            try
            {
                return new ReportOptions
                           {
                               assembly_to_scan = assembly_resolver.find_using(args[0]),
                               observation_specification = observation_specification_factory.create_from(args[1]),
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
