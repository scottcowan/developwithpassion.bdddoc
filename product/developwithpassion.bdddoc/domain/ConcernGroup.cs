using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdddoc.utility;

namespace developwithpassion.bdddoc.domain
{
    public interface IConcernGroup : ITypeForAConcern, IGroupingOfConcerns
    {
        IEnumerable<IConcern> concerns { get; set; }
        string story_key { get; }
    }

    public class ConcernGroup : IConcernGroup
    {
        private IEnumerable<IConcern> all_concerns;

        public ConcernGroup(IEnumerable<IConcern> concerns)
        {
            all_concerns = concerns;
        }


        public IEnumerable<IConcern> concerns
        {
            get { return all_concerns.one_at_a_time(); }
            set { all_concerns = value; }
        }

        public Type concerned_with
        {
            get { return all_concerns.First().concerned_with; }
        }

        public string story_key
        {
            get { return all_concerns.First().story_key; }
        }

        public int total_number_of_concerns
        {
            get { return all_concerns.Count(); }
        }

        public int total_number_of_observations
        {
            get { return all_concerns.Sum(x => x.total_number_of_observations); }
        }
    }
}