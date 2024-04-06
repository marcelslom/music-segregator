using CommandLine;
using MusicSegregator.Options;
using MusicSegregator.Services;
using NLog;

namespace MusicSegregator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<CliOptions>(args);
            if (result.Tag == ParserResultType.Parsed)
            {
                var context = Context.From(result.Value);
                LogManager.Configuration.Variables["infoEnabled"] = context.CreateLogFiles.ToString();
                LogManager.Configuration.Variables["baseDir"] = context.SourcePath;
                var segregator = new SegregatorService(context);
                segregator.Start();
            } 
            else
            {
                //todo handle parsing error
            }
        }
    }
}
