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
                Context.Instance.LoadOptions(result.Value);
            } 
            else
            {
                //todo handle parsing error
            }
        }
    }
}
