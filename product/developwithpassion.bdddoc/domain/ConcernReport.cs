using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdddoc.utility;

namespace developwithpassion.bdddoc.domain
{
    public interface IConcernReport : IGroupingOfConcerns
    {
        IEnumerable<IStoryReport> stories { get; set; }
        int total_number_of_groups { get; }
    }

    public class ConcernReport : IConcernReport
    {
        private IEnumerable<IStoryReport> user_stories;

        public ConcernReport(IEnumerable<IStoryReport> stories)
        {
            this.user_stories = stories;
        }

        public int total_number_of_groups
        {
            get { return user_stories.Sum(x => x.groups.Count()); }
        }

        public int total_number_of_concerns
        {
            get { return user_stories.Sum(x => x.total_number_of_concerns); }
        }

        public int total_number_of_observations
        {
            get { return user_stories.Sum(x => x.total_number_of_observations); }
        }

        public IEnumerable<IStoryReport> stories
        {
            get { return user_stories.one_at_a_time(); }
            set { user_stories = value; }
        }
    }
}