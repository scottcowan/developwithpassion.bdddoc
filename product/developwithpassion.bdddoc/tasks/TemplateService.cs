using System;
using System.Collections.Generic;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Exception;

namespace developwithpassion.bdddoc.tasks
{
    public interface ITemplateService
    {
        string generate(string templateFile,Dictionary<string,object> values);
    }

    public class TemplateService : ITemplateService
    {
        TextWriter logger;

        public TemplateService() : this(Console.Out)
        {
        }

        public TemplateService(TextWriter logger)
        {
            this.logger = logger;
        }

        public string generate(string templateFile,Dictionary<string,object> values)
        {
            string output = "";
            try
            {
                Velocity.Init();
                VelocityContext context = new VelocityContext();
                foreach (var value in values)
                    context.Put(value.Key, value.Value);

                Template template = null;
                try
                {
                    template = Velocity.GetTemplate(templateFile);
                }
                catch (ResourceNotFoundException)
                {
                    logger.WriteLine("cannot find template " + templateFile);
                }
                catch (ParseErrorException pee)
                {
                    logger.WriteLine("Syntax error in template " + templateFile + ":" + pee);
                }

                if (template != null)
                {
                    var writer = new StringWriter();
                    template.Merge(context, writer);
                    output = writer.GetStringBuilder().ToString();
                }
            }
            catch (System.Exception e)
            {
                System.Console.Out.WriteLine(e);
            }
            return output;        
        }
    }
}