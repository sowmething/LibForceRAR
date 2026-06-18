using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LibForceRar
{
    public class ForceRar
    {
        // Single-threaded brute-force
        public static string Bruteforce(string rarPath, long maxNumber)
        {
            string tempDir = CreateTempDirectory();
            try
            {
                string password = TryBruteforce(rarPath, maxNumber, tempDir, null);
                return password;
            }
            finally
            {
                CleanupTempDirectory(tempDir);
            }
        }

        // Multi-threaded brute-force
        public static string BruteforceThread(string rarPath, long maxNumber, int threadCount)
        {
            string tempDir = CreateTempDirectory();
            try
            {
                string password = TryBruteforceThreaded(rarPath, maxNumber, threadCount, tempDir, null);
                return password;
            }
            finally
            {
                CleanupTempDirectory(tempDir);
            }
        }

        // Single-threaded with custom UnRAR path
        public static string BruteforceCustomUnrar(string rarPath, long maxNumber, string unrarPath)
        {
            string tempDir = CreateTempDirectory();
            try
            {
                string password = TryBruteforce(rarPath, maxNumber, tempDir, unrarPath);
                return password;
            }
            finally
            {
                CleanupTempDirectory(tempDir);
            }
        }

        // Multi-threaded with custom UnRAR path
        public static string BruteforceCustomUnrarThread(string rarPath, long maxNumber, string unrarPath, int threadCount)
        {
            string tempDir = CreateTempDirectory();
            try
            {
                string password = TryBruteforceThreaded(rarPath, maxNumber, threadCount, tempDir, unrarPath);
                return password;
            }
            finally
            {
                CleanupTempDirectory(tempDir);
            }
        }

        private static string CreateTempDirectory()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);
            return tempPath;
        }

        private static void CleanupTempDirectory(string tempDir)
        {
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch { 
            
            }
        }

        private static string TryBruteforce(string rarPath, long maxNumber, string tempDir, string customUnrarPath)
        {
            if (!File.Exists(rarPath)) throw new FileNotFoundException("RAR file not found.", rarPath);
            if (customUnrarPath != null && !File.Exists(customUnrarPath)) throw new FileNotFoundException("UnRAR executable not found.", customUnrarPath);

            string unrarExe = customUnrarPath ?? "unrar.exe";
            for (long i = 0; i <= maxNumber; i++)
            {
                string password = i.ToString();
                if (TestPassword(rarPath, password, tempDir, unrarExe))
                {
                    return password;
                }
            }
            return null;
        }

        private static string TryBruteforceThreaded(string rarPath, long maxNumber, int threadCount, string tempDir, string customUnrarPath)
        {
            if (!File.Exists(rarPath)) throw new FileNotFoundException("RAR file not found.", rarPath);
            if (customUnrarPath != null && !File.Exists(customUnrarPath)) throw new FileNotFoundException("UnRAR executable not found.", customUnrarPath);

            string unrarExe = customUnrarPath ?? "unrar.exe";
            string foundPassword = null;
            object lockObject = new object();
            long numbersPerThread = maxNumber / threadCount;
            Task[] tasks = new Task[threadCount];

            for (int t = 0; t < threadCount; t++)
            {
                long start = t * numbersPerThread;
                long end = (t == threadCount - 1) ? maxNumber : start + numbersPerThread - 1;

                tasks[t] = Task.Run(() =>
                {
                    for (long i = start; i <= end; i++)
                    {
                        lock (lockObject)
                        {
                            if (foundPassword != null) return;
                        }

                        string password = i.ToString();
                        if (TestPassword(rarPath, password, tempDir, unrarExe))
                        {
                            lock (lockObject)
                            {
                                foundPassword = password;
                            }
                            return;
                        }
                    }
                });
            }

            Task.WaitAll(tasks);
            return foundPassword;
        }

        private static bool TestPassword(string rarPath, string password, string tempDir, string unrarExe)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = unrarExe,
                    Arguments = $"x -p{password} \"{rarPath}\" \"{tempDir}\" -y",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

// I hope this is helpful for you
