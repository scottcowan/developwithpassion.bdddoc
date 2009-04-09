using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdddoc.utility;

namespace developwithpassion.bdddoc.domain
{
    public interface IConcern : ITypeForAConcern
    {
        BDDStyleName name { get; }
        IEnumerable<IConcernObservation> observations { get; }
        int total_number_of_observations { get; }
    }

    public class Concern : IConcern
    {
        private IEnumerable<IConcernObservation> all_observations;
        public virtual Type concerned_with { get; private set; }
        public BDDStyleName name { get; private set; }

        public Concern(Type target_concern, BDDStyleName concern_name, IEnumerable<IConcernObservation> observations)
        {
            this.all_observations = observations;
            this.concerned_with = target_concern;
            this.name = concern_name;
        }

        public IEnumerable<IConcernObservation> observations
        {
            get { return all_observations.one_at_a_time(); }
        }

        public int total_number_of_observations
        {
            get { return all_observations.Count(); }
        }
    }
}