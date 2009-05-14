using System.Collections.Generic;
using System.Linq;

namespace developwithpassion.bdddoc.domain
{
    public interface IStoryFactory
    {
        IStoryReport create_from_concerns(IEnumerable<IConcern> grouping);
    }

    public class StoryFactory : IStoryFactory
    {
        public IStoryReport create_from_concerns(IEnumerable<IConcern> story_concerns)
        {
            return
                new StoryReport(groups_of_concerns(story_concerns));
        }

        private IEnumerable<IConcernGroup> groups_of_concerns(IEnumerable<IConcern> concerns)
        {
            return concerns
                .GroupBy(x => x.concerned_with)
                .Select(x => new ConcernGroup(x))
                .Cast<IConcernGroup>();
        }
    }
}