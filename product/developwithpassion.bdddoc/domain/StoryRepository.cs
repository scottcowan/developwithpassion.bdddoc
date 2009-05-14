using System;
using System.Collections.Generic;
using System.Linq;

namespace developwithpassion.bdddoc.domain
{
    public interface IStoryRepository
    {
        IEnumerable<IStoryReport> all_concern_groups_found_using(IReportOptions options, IObservationReport observations);
    }

    public class StoryRepository : IStoryRepository
    {
        private readonly IStoryFactory StoryFactory;
        private readonly IConcernFactory concern_factory;

        public StoryRepository() : this(new StoryFactory(),new ConcernFactory())
        {
        }

        public StoryRepository(IStoryFactory StoryFactory, IConcernFactory concern_factory)
        {
            this.StoryFactory = StoryFactory;
            this.concern_factory = concern_factory;
        }

        public IEnumerable<IStoryReport> all_concern_groups_found_using(IReportOptions options, IObservationReport observations)
        {
            var concerns = options.assembly_to_scan
                .all_types_with_a_concern_attribute()
                .Select(x => concern_factory.create_concern_from(x, options.observation_specification,observations))
                .Cast<IConcern>();
            
            var stories = concerns
                .GroupBy(x => x.story_key)
                .Select(x=> StoryFactory.create_from_concerns(x))                
                .Cast<IStoryReport>();

            return stories;
        }
    }

}
