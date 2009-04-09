using System.IO;
using System.Linq;
using System.Text;
using developwithpassion.bdddoc.utility;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tasks
{
    public interface IReportWriter
    {
        void save(IConcernReport report, string file_name);
    }

    public class SimpleHtmlReportWriter : IReportWriter
    {
        private const string logo_filename = "developwithpassion.bdddoc-logo.jpg";
        private const string css_filename = "developwithpassion.bdddoc.css";
        
        public void save(IConcernReport report, string file_name)
        {
            File.WriteAllText(file_name, build_report_output_using(report));
            copy_report_assets(file_name);
        }

        private void copy_report_assets(string file_name)
        {
            var base_path = build_path(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var output_path = build_path(file_name);


            var logofile = Path.Combine(base_path, logo_filename);
            var target_logo = Path.Combine(output_path, logo_filename);

            
            var cssfile = Path.Combine(base_path, css_filename);
            var target_css = Path.Combine(output_path, css_filename);

            if(File.Exists(logofile) && !File.Exists(target_logo))
                File.Copy(logofile,target_logo);
            if(File.Exists(cssfile) && !File.Exists(target_css))
                File.Copy(cssfile,target_css);
        }

        private string build_path(string file_name)
        {
            return new FileInfo(file_name).Directory.FullName;
        }

        private string build_report_output_using(IConcernReport report)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("<html><head><link media=\"all\" rel=\"stylesheet\" type=\"text/css\" href=\"{0}\"></link></head><body>",css_filename);
            builder.AppendFormat("<img src=\"{0}\" />",logo_filename);
            builder.Append(build_report_header_block_using(report));
            builder.AppendFormat("<ul class=\"behaviour\">");
            report.groups.OrderBy(x => x.concerned_with.Name).each(rg => builder.Append(build_behaviour_block_using(rg)));
            builder.AppendFormat("</ul>");
            builder.Append("</body></html>");

            return builder.ToString();
        }

        private string build_report_header_block_using(IConcernReport report)
        {
            return string.Format("<h1>Concerns: {0} - Observations: {1}</h1>", report.total_number_of_concerns, report.total_number_of_observations);
        }

        private string build_behaviour_block_using(IConcernGroup concern_group)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("<li><h1>Behaviour of: {0} [ {1} Concern(s) , {2} Observation(s) ]</h1>", concern_group.concerned_with.Name, concern_group.total_number_of_concerns,
                                 concern_group.total_number_of_observations);
            builder.AppendFormat("<ul class=\"concern\">");
            concern_group.concerns.OrderBy(x => x.name.name).each(cg => builder.Append(build_concern_block_using(cg)));
            builder.Append("</ul></li>");
            return builder.ToString();
        }

        private string build_concern_block_using(IConcern concern)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("<li><h2>{0}</h2>", concern.name);
            builder.Append("<ul class=\"observation\">");
            concern.observations.OrderBy(x => x.name.name).each(x => builder.AppendFormat("<li class='{0}'>{1}</li>",x.success?"pass":"fail", x.name));
            builder.Append("</ul></li>");

            return builder.ToString();
        }
    }
}
