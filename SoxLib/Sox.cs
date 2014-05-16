using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using log4net;
using SoxLib.Helpers;

namespace SoxLib
{
    public class Sox
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Sox));

        public string SoxDirectory { get; set; }
        public string TempDir
        {
            get
            {
                var tempDir = Path.Combine(Path.GetTempPath(), "sox");
                Directory.CreateDirectory(tempDir);
                return tempDir;
            }
        }

        public Stream Convert(Stream input, ConvertOptions options)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            string outputExtension = GetFileExtension(options.OutputFileInfo.FileType);
            string inputExtensions = GetFileExtension(options.InputFileInfo.FileType);

            var inputTempFileName = Path.Combine(TempDir, Guid.NewGuid() + inputExtensions);
            input.WriteToFile(inputTempFileName);
            var outputTempFileName = Path.Combine(TempDir, Guid.NewGuid() + outputExtension);
            var cmdLineArgs = BuildConvertCommandLineArgs(inputTempFileName, outputTempFileName, options);
            var soxExeLocation = Path.GetFullPath(Path.Combine(SoxDirectory, "sox.exe"));
            RunSox(soxExeLocation, cmdLineArgs);
            File.Delete(inputTempFileName);
            return new DeleteFileStream(outputTempFileName);
        }

        public Stream Trim(Stream input, TrimOptions options)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            string outputExtension = GetFileExtension(options.OutputFileInfo.FileType);
            string inputExtensions = GetFileExtension(options.InputFileInfo.FileType);

            var inputFileName = Path.Combine(TempDir, Guid.NewGuid() + inputExtensions);
            input.WriteToFile(inputFileName);
            var outputFileName = Path.Combine(TempDir, Guid.NewGuid() + outputExtension);
            var cmdLineArgs = BuildTrimCommandLineArgs(inputFileName, outputFileName, options);
            var fileName = Path.GetFullPath(Path.Combine(SoxDirectory, "sox.exe"));
            RunSox(fileName, cmdLineArgs);
            File.Delete(inputFileName);
            return new DeleteFileStream(outputFileName);
        }

        private void RunSox(string fileName, string cmdLineArgs)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception("Could not find sox: " + fileName);
            }

            _log.Debug("Running Sox: " + fileName + " " + cmdLineArgs);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = cmdLineArgs,
                UseShellExecute = false,
                ErrorDialog = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            Process process = new Process
            {
                StartInfo = startInfo
            };
            if (!process.Start())
            {
                throw new Exception("Could not start process \"" + fileName + "\"");
            }
            StreamReader outputReader = process.StandardOutput;
            StreamReader errorReader = process.StandardError;
            process.WaitForExit();

            _log.Debug("Standard Output:\n" + outputReader.ReadToEnd());
            _log.Error("Standard Error:\n" + errorReader.ReadToEnd());
        }

        private string BuildTrimCommandLineArgs(string inputFileName, string outputFileName, TrimOptions options)
        {
            StringBuilder result = new StringBuilder();
            result.Append(BuildCommandLineArgs(options.InputFileInfo));
            result.Append(" " + inputFileName);
            result.Append(BuildCommandLineArgs(options.OutputFileInfo));
            result.Append(" " + outputFileName);
            result.Append(" trim");
            result.Append(" " + options.StartTime.ToCommandLineArg());
            if (options.Length != null && options.End != null)
            {
                throw new Exception("Only length or end is allowed not both");
            }
            if (options.Length != null)
            {
                result.Append(" " + options.Length.ToCommandLineArg());
            }
            else if (options.End != null)
            {
                result.Append(" =" + options.End.ToCommandLineArg());
            }
            return result.ToString();
        }

        private string BuildConvertCommandLineArgs(string inputFileName, string outputFileName, ConvertOptions options)
        {
            StringBuilder result = new StringBuilder();
            result.Append(BuildCommandLineArgs(options.InputFileInfo));
            result.Append(" " + inputFileName);
            result.Append(BuildCommandLineArgs(options.OutputFileInfo));
            result.Append(" " + outputFileName);
            return result.ToString();
        }

        private string BuildCommandLineArgs(FileInfo fileInfo)
        {
            StringBuilder result = new StringBuilder();
            if (fileInfo.Channels != null)
            {
                result.Append(" -c " + fileInfo.Channels.Value);
            }
            if (fileInfo.SamplingRate != null)
            {
                result.Append(" -r " + fileInfo.SamplingRate.Value);
            }
            if (fileInfo.EncodingType != null)
            {
                switch (fileInfo.EncodingType)
                {
                    case EncodingType.UnsignedInteger: result.Append(" -u"); break;
                    default:
                        throw new Exception("Unhandled encoding type: " + fileInfo.EncodingType.Value);
                }
            }
            if (fileInfo.SampleSizeInBits != null)
            {
                result.Append(" -b " + fileInfo.SampleSizeInBits.Value);
            }
            return result.ToString();
        }

        private string GetFileExtension(FileType? fileType)
        {
            if (fileType == null)
            {
                return ".audio";
            }
            switch (fileType.Value)
            {
                case FileType.RawUnsignedInteger8: return ".u8";
                case FileType.Wav: return ".wav";
                case FileType.Mp3: return ".mp3";
                default:
                    throw new Exception("Unhandled file type: " + fileType.Value);
            }
        }

        public static FileType GetFileTypeFromExtension(string extension)
        {
            extension = extension.TrimStart('.').ToLowerInvariant();
            switch (extension)
            {
                case "mp3": return FileType.Mp3;
                case "wav": return FileType.Wav;
                case "u8": return FileType.RawUnsignedInteger8;
                default:
                    throw new Exception("Unknown audio file extension '" + extension + "'");
            }
        }
    }
}
