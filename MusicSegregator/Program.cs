using CommandLine;

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
                var segregator = new Segregator(context);
                segregator.Start();
            } 
            else
            {
                //todo handle parsing error
            }
        }
    }
}
