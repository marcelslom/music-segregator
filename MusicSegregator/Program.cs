using CommandLine;
using CommandLine.Text;
using MusicSegregator.Options;
using MusicSegregator.Services;
using NLog;

namespace MusicSegregator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = null);
            var result = parser.ParseArguments<CliOptions>(args);
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
                DisplayHelp(result);
            }
        }

        private static List<string> postOptionsLines =
            [
            "Parameters available for file renaming:",
            "  AlbumArtists",
            "  Performers",
            "  Composers",
            "  Genres",
            "  Title",
            "  Album",
            "  Year",
            "  Track",
            "  TrackCount",
            "  Disc",
            "  DiscCount",
            "  InitialKey",
            "  BeatsPerMinute"
            ];

        private static void DisplayHelp<T>(ParserResult<T> result)
        {
            HelpText helpText = null;
            if (result.Errors.IsVersion())
                helpText = HelpText.AutoBuild(result);
            else
            {
                helpText = HelpText.AutoBuild(result, h =>
                {
                    h.AddNewLineBetweenHelpSections = true;
                    h.AddPostOptionsLines(postOptionsLines);
                    return h;
                });
            };
            Console.WriteLine(helpText);
        }
    }
}
