using System.Collections.Generic;
using developwithpassion.bdddoc.utility;

namespace developwithpassion.bdddoc.domain
{
    public static class StoryReportExtensions
    {
        public static IEnumerable<IConcernGroup> all_concern_groups(this IEnumerable<IStoryReport> stories)
        {
            foreach (var story in stories)
            {
                foreach (var concern_group in story.groups)
                {
                    yield return concern_group;
                }                
            }
        }
    }
}