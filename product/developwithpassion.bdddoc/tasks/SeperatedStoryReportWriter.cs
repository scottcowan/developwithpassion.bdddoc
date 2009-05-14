using System;
using System.Collections.Generic;
using System.IO;
using developwithpassion.bdddoc.domain;

namespace developwithpassion.bdddoc.tasks
{
    public class SeperatedStoryReportWriter : IReportWriter
    {
        readonly ITemplateService service;

        public SeperatedStoryReportWriter() : this(new TemplateService())
        {
        }

        SeperatedStoryReportWriter(ITemplateService service)
        {
            this.service = service;
        }

        public void save(IConcernReport report, string file_name)
        {
            var location = new FileInfo(file_name).DirectoryName;
            var story_files = new List<string>(); 
            foreach (var story in report.stories)
            {
                story_files.Add(story.story_key);
                save_seperated_file(story,location);
            }
            save_index(file_name, report,story_files);
        }

        public void save_seperated_file(IStoryReport story, string location)
        {
            var values = new Dictionary<string, object>
                             {                                
                                
                                { "Story", story }
                             };
            var story_file = story.story_key == "" ? "default.htm" : story.story_key + ".htm";
            File.WriteAllText(Path.Combine(location, story_file), service.generate("seperated.vm", values));
        }

        public void save_index(string file_name,IConcernReport report, IList<string> stories)
        {
            var values = new Dictionary<string, object>
                             {
                                 { "Report", report },
                                 { "Stories",stories}
                             };
            File.WriteAllText(file_name,service.generate("seperated-index.vm",values));
        }
    }
}