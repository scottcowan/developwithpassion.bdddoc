using System.Collections.Generic;
using System.Linq;

namespace developwithpassion.bdddoc.domain
{
    public interface IConcernGroupRepository
    {
        IEnumerable<IConcernGroup> all_concern_groups_found_using(IReportOptions options, IObservationReport observations);
    }

    public class ConcernGroupRepository : IConcernGroupRepository
    {
        private readonly IConcernFactory concern_factory;

        public ConcernGroupRepository() : this(new ConcernFactory())
        {
        }

        public ConcernGroupRepository(IConcernFactory concern_factory)
        {
            this.concern_factory = concern_factory;
        }

        public IEnumerable<IConcernGroup> all_concern_groups_found_using(IReportOptions options, IObservationReport observations)
        {
            return options.assembly_to_scan
                .all_types_with_a_concern_attribute()
                .Select(x => concern_factory.create_concern_from(x, options.observation_specification,observations))
                .GroupBy(x => x.concerned_with)
                .Select(x => new ConcernGroup(x))
                .Cast<IConcernGroup>();
        }
    }
}
