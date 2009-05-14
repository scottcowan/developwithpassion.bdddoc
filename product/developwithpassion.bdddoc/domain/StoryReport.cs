using System;
using System.Collections.Generic;
using System.Linq;

namespace developwithpassion.bdddoc.domain
{
    public interface IStoryReport
    {
        string story_key { get; }
        IEnumerable<IConcernGroup> groups { get; set; }
        int total_number_of_concerns { get; }
        int total_number_of_observations { get; }
    }

    public class StoryReport : IStoryReport
    {
        public StoryReport(IEnumerable<IConcernGroup> groups)
        {
            this.groups = groups;
        }

        public IEnumerable<IConcernGroup> groups { get; set; }
        public string story_key
        {
            get
            {
                return groups.First().story_key;
            }
        }

        public int total_number_of_concerns
        {
            get { return groups.Sum(x => x.total_number_of_concerns); }
        }

        public int total_number_of_observations
        {
            get { return groups.Sum(x => x.total_number_of_observations); }
        }
    }
}