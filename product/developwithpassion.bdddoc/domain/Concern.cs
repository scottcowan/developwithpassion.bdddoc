using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdddoc.utility;

namespace developwithpassion.bdddoc.domain
{
    public interface IConcern : ITypeForAConcern
    {
        BDDStyleName name { get; set; }
        IEnumerable<IConcernObservation> observations { get; set; }
        string story_key { get; set; }
        int total_number_of_observations { get; }
    }

    public class Concern : IConcern
    {
        private IEnumerable<IConcernObservation> all_observations;
        public virtual Type concerned_with { get;  set; }
        public string story_key { get; set; }
        public BDDStyleName name { get;  set; }

        public Concern(Type target_concern, string story_key, BDDStyleName concern_name, IEnumerable<IConcernObservation> observations)
        {
            this.all_observations = observations;
            this.story_key = story_key;
            this.concerned_with = target_concern;
            this.name = concern_name;
        }

        public IEnumerable<IConcernObservation> observations
        {
            get { return all_observations.one_at_a_time(); }
            set { all_observations = value; }
        }

        public int total_number_of_observations
        {
            get { return all_observations.Count(); }
        }
    }
}