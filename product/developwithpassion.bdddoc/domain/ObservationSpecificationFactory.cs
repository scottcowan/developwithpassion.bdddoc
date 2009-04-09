namespace developwithpassion.bdddoc.domain
{
    public class ObservationSpecificationFactory : IObservationSpecificationFactory
    {
        public IObservationSpecification create_from(string observation_attribute)
        {
            return new OrObservationSpecification(new ItFieldObservationSpecification(), new ObservationAttributeSpecification(observation_attribute));
        }
    }
}