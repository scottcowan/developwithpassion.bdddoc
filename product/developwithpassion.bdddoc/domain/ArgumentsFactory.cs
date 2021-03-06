using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace bdddoc.domain
{
    public class ArgumentsFactory
    {
        private IDictionary<string,string> Parameters;

        public ArgumentsFactory(string[] Args)
        {
            Parameters = new Dictionary<string,string>();
            Regex Spliter = new Regex(@"^-{1,2}|=",
                                      RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Regex Remover = new Regex(@"^['""]?(.*?)['""]?$",
                                      RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string Parameter = null;
            string[] Parts;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))

            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" 
            //   /param4=happy -param5 '--=nice=--'

            foreach (string Txt in Args)
            {
                // Look for new parameters (-,/ or --) and a
                // possible enclosed value (=,:)

                Parts = Spliter.Split(Txt, 3);
                        // Found a value (for the last parameter 
                        // found (space separator))
                    if(Parts.Length == 1)
                    {
                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                            {
                                Parts[0] = Remover.Replace(Parts[0], "$1");
                                Parameters.Add(Parameter, Parts[0]);
                            }
                            Parameter = null;
                        }
                        // else Error: no parameter waiting for a value (skipped)
                    }
                    if(Parts.Length==2)
                    {
                        // The last parameter is still waiting. 

                        // With no value, set it to true.

                        if (Parameter != null)
                        {
                            if (!Parameters.ContainsKey(Parameter))
                                Parameters.Add(Parameter, "true");
                        }
                        Parameter = Parts[1];                        
                        // Parameter with enclosed value
                    }
                if(Parts.Length==3)
                {
                    // The last parameter is still waiting. 

                    // With no value, set it to true.

                    if (Parameter != null)
                    {
                        if (!Parameters.ContainsKey(Parameter))
                            Parameters.Add(Parameter, "true");
                    }

                    Parameter = Parts[1];

                    // Remove possible enclosing characters (",')

                    if (!Parameters.ContainsKey(Parameter))
                    {
                        Parts[2] = Remover.Replace(Parts[2], "$1");
                        Parameters.Add(Parameter, Parts[2]);
                    }

                    Parameter = null;
                }                                
            }
            // In case a parameter is still waiting

            if (Parameter != null)
            {
                if (!Parameters.ContainsKey(Parameter))
                    Parameters.Add(Parameter, "true");
            }
        }

        public string this[string Param]
        {
            get { return (Parameters[Param]); }
        } 
    }
}
