namespace developwithpassion.bdddoc.domain
{
    public interface IConcernReportFactory
    {
        IConcernReport create_using(IReportOptions options, IObservationReport observations);
    }

    public class ConcernReportFactory : IConcernReportFactory
    {
        private readonly IConcernGroupRepository concern_group_repository;

        public ConcernReportFactory() : this(new ConcernGroupRepository())
        {
        }

        public ConcernReportFactory(IConcernGroupRepository concern_group_repository)
        {
            this.concern_group_repository = concern_group_repository;
        }

        public IConcernReport create_using(IReportOptions options, IObservationReport observations)
        {
            return new ConcernReport(concern_group_repository.all_concern_groups_found_using(options,observations));
        }
    }
}
