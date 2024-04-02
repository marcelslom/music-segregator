﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSegregator
{
    internal class CliOptions : IOptions
    {
        [Option('s', "source", Required = false, HelpText = "Path of source folder")]
        public string SourcePath { get; set; }

        [Option('d', "destination", Required = false, HelpText = "Set output to verbose messages.")]
        public string DestinationPath { get; set; }

        [Option('m', "move-files", Required = false, Default = false, HelpText = "Set output to verbose messages.")]
        public bool DeleteSourceFile { get; set; }

        [Option('f', "filename-schema", Required = false, HelpText = "Set output to verbose messages.")]
        public string FilenameSchema { get; set; }

        [Option('l', "log", Required = false, Default = false, HelpText = "Set output to verbose messages.")]
        public bool CreateLogFiles { get; set; }
    }
}
