namespace developwithpassion.bdddoc.domain
{
    public interface IConcernReportFactory
    {
        IConcernReport create_using(IReportOptions options, IObservationReport observations);
    }

    public class ConcernReportFactory : IConcernReportFactory
    {
        private readonly IStoryRepository StoryRepository;

        public ConcernReportFactory() : this(new StoryRepository())
        {
        }

        public ConcernReportFactory(IStoryRepository StoryRepository)
        {
            this.StoryRepository = StoryRepository;
        }

        public IConcernReport create_using(IReportOptions options, IObservationReport observations)
        {
            return new ConcernReport(StoryRepository.all_concern_groups_found_using(options,observations));
        }
    }
}
