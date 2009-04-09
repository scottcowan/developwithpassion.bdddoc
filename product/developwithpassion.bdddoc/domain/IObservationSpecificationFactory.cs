namespace developwithpassion.bdddoc.domain
{
    public interface IObservationSpecificationFactory
    {
        IObservationSpecification create_from(string observation_attribute);
    }
}