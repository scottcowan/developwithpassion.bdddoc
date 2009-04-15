using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using developwithpassion.bdddoc.utility;

namespace developwithpassion.bdddoc.domain
{
    public interface ITestReportFactory
    {
        IObservationReport create_using(IReportOptions report_options);
    }

    public class MbUnitReportFactory : ITestReportFactory
    {
        readonly ITestReportRepository repository;

        public MbUnitReportFactory() : this(new MbUnitReportRepository()) {}

        MbUnitReportFactory(ITestReportRepository repository)
        {
            this.repository = repository;
        }

        public IObservationReport create_using(IReportOptions report_options)
        {
            return new ObservationReport(repository.all_observations(report_options.mbunit_test_report));
        }
    }

    public interface ITestReportRepository
    {
        IEnumerable<IObservation> all_observations(string report_file);
    }

    public interface IObservationReport
    {
        IEnumerable<IObservation> observations { get; }
        IObservation from_full_name(string name);
    }

    public class ObservationReport : IObservationReport
    {
        public IEnumerable<IObservation> observations { get; private set; }

        public ObservationReport(IEnumerable<IObservation> observations)
        {
            this.observations = observations;
        }

        public IObservation from_full_name(string name)
        {
            IList<IObservation> list = new List<IObservation>(observations);
            foreach (var observation in list)
            {
                if (observation.name == name)
                    return observation;
            }
            return new ObservationResult();
        }
    }

    public interface IObservation
    {
        string name { get; set; }
        bool success { get; set; }
    }

    public class ObservationResult : IObservation
    {
        public string name { get; set; }
        public bool success { get; set; }
    }

    public class MbUnitReportRepository : ITestReportRepository
    {
        public IEnumerable<IObservation> all_observations(string report_file)
        {
            if (File.Exists(report_file))
            {
                var doc = XDocument.Load(report_file);
                var descendants = doc.Descendants("run");
                return descendants.for_each<XElement, IObservation>(
                    x => new ObservationResult
                         {
                             name = strip_nested_class_name_from( x.Attribute("name").Value.Replace(".tear_down", "").Replace(".setup", "") ),
                             success = x.Attribute("result").Value == "success"
                         });
            }
            Console.WriteLine("couldn't find " + report_file);
            return new List<IObservation> {new ObservationResult()};
        }

        private string strip_nested_class_name_from( string observation )
        {
            return observation.Split('+').Last();
        }
    }
}