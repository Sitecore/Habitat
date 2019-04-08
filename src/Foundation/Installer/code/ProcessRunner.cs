namespace Sitecore.Foundation.Installer
{
    using System.ComponentModel;
    using System.Diagnostics;
    using Sitecore.Diagnostics;

    public class ProcessRunner : IProcessRunner
    {
        public string LogPrefix { get; set; }

        public void Run(string commandPath, string arguments)
        {
            var processStartInfo = new ProcessStartInfo(commandPath, arguments)
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            this.RunProcess(processStartInfo);
        }

        protected virtual void RunProcess(ProcessStartInfo processStartInfo)
        {
            using (var process = new Process
            {
                StartInfo = processStartInfo
            })
            {
                process.OutputDataReceived += this.ReadOutputLine;
                string error;

                try
                {
                    process.Start();
                    process.BeginOutputReadLine();
                    error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                }
                catch (Win32Exception ex)
                {
                    throw new ProcessException($"Failed to process command {processStartInfo.FileName} {processStartInfo.Arguments}", ex);
                }

                if (process.ExitCode != 0 && !string.IsNullOrEmpty(error))
                {
                    throw new ProcessException($"Failed to process command {processStartInfo.FileName} {processStartInfo.Arguments} \r\n Error text: {error}");
                }
            }
        }

        private void ReadOutputLine(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Log.Debug($"{this.LogPrefix} {e.Data}", this);
            }
        }
    }
}