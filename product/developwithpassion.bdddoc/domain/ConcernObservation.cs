namespace developwithpassion.bdddoc.domain
{
    public interface IConcernObservation
    {
        BDDStyleName name { get; }
        bool success { get; }
    }

    public class ConcernObservation : IConcernObservation
    {
        public BDDStyleName name { get; private set; }
        public bool success { get; private set; }

        public ConcernObservation(BDDStyleName name, bool success)
        {
            this.name = name;
            this.success = success;
        }
    }
}
