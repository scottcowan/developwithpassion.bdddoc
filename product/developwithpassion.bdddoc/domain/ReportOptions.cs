using System;
using System.Reflection;

namespace developwithpassion.bdddoc.domain
{
    public interface IReportOptions
    {
        Assembly assembly_to_scan { get; set; }
        IObservationSpecification observation_specification { get; set; }
        string output_filename { get; set; }
        bool is_valid { get; }
        string mbunit_test_report { get; set; }
    }

    public class MissingReportOptions : IReportOptions
    {
        public bool is_valid
        {
            get { return false; }
        }

        public string mbunit_test_report
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public Assembly assembly_to_scan
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IObservationSpecification observation_specification
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string output_filename
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

    public class ReportOptions : IReportOptions
    {
        public string mbunit_test_report { get; set; }
        public Assembly assembly_to_scan { get; set; }

        public IObservationSpecification observation_specification { get; set; }
        public string output_filename { get; set; }

        public bool is_valid
        {
            get { return true; }
        }
    }
}
