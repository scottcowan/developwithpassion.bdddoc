using System.Reflection;

namespace developwithpassion.bdddoc.domain
{
    public interface IAssemblyRepository
    {
        Assembly find_using(string assembly_filename);
    }

    public class AssemblyRepository : IAssemblyRepository
    {
        public Assembly find_using(string assembly_filename)
        {
            return Assembly.LoadFrom(assembly_filename);
        }
    }
}