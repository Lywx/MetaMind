namespace FileSearcher
{
    using System;
    using System.IO;
    using System.Threading;

    public class Searcher
    {
        private Thread thread;

        private bool stopped;

        private SearcherParams searchParams;

        private byte[] containingBytes;

        #region Asynchronous Events 

        public delegate void ThreadEndedEventHandler(ThreadEndedEventArgs e);

        public delegate void FoundInfoEventHandler(FoundInfoEventArgs e);

        public event FoundInfoEventHandler FoundInfo;

        public event ThreadEndedEventHandler ThreadEnded;

        #endregion

        public bool Start(SearcherParams pars)
        {
            var success = false;

            if (this.thread == null)
            {
                // Perform a reset of all variables,
                // to ensure that the state of the searcher is the same on every new start:
                this.ResetVariables();

                // Remember the parameters:
                this.searchParams = pars;

                // Start searching for FileSystemInfos that match the parameters:
                this.thread = new Thread(this.SearchThread);
                this.thread.Start();

                success = true;
            }

            return success;
        }

        public void Stop()
        {
            // Stop the thread by setting a flag:
            this.stopped = true;
        }


        private void ResetVariables()
        {
            this.thread          = null;
            this.stopped         = false;
            this.searchParams    = null;
            this.containingBytes = null;
        }

        private void SearchThread()
        {
            var success = true;
            var errorMsg = "";

            // Search for FileSystemInfos that match the parameters:
            if ((this.searchParams.SearchDir.Length >= 3) && Directory.Exists(this.searchParams.SearchDir))
            {
                if (this.searchParams.FileNames.Count > 0)
                {
                    // Convert the string to search for into bytes if necessary:
                    if (this.searchParams.ContainingChecked)
                    {
                        if (this.searchParams.ContainingText != "")
                        {
                            try
                            {
                                containingBytes = this.searchParams.Encoding.GetBytes(this.searchParams.ContainingText);
                            }
                            catch (Exception)
                            {
                                success = false;
                                errorMsg = "The string\r\n" + this.searchParams.ContainingText + "\r\ncannot be converted into bytes.";
                            }
                        }
                        else
                        {
                            success = false;
                            errorMsg = "The string to search for must not be empty.";
                        }
                    }

                    if (success)
                    {
                        // Get the directory info for the search directory:
                        DirectoryInfo dirInfo = null;
                        try
                        {
                            dirInfo = new DirectoryInfo(this.searchParams.SearchDir);
                        }
                        catch (Exception ex)
                        {
                            success = false;
                            errorMsg = ex.Message;
                        }

                        if (success)
                        {
                            // Search the directory (maybe recursively),
                            // and raise events if something was found:
                            this.SearchDirectory(dirInfo);
                        }
                    }
                }
                else
                {
                    success = false;
                    errorMsg = "Please enter one or more filenames to search for.";
                }
            }
            else
            {
                success = false;
                errorMsg = "The directory\r\n" + this.searchParams.SearchDir + "\r\ndoes not exist.";
            }

            // Remember the thread has ended:
            this.thread = null;

            // Raise an event:
            if (this.ThreadEnded != null)
            {
                this.ThreadEnded(new ThreadEndedEventArgs(success, errorMsg));
            }
        }

        private void SearchDirectory(DirectoryInfo dirInfo)
        {
            if (!this.stopped)
            {
                try
                {
                    foreach (var fileName in this.searchParams.FileNames)
                    {
                        var infos = dirInfo.GetFileSystemInfos(fileName);

                        foreach (var info in infos)
                        {
                            if (this.stopped)
                            {
                                break;
                            }

                            if (this.MatchesRestrictions(info))
                            {
                                // We have found a matching FileSystemInfo, so let's raise an event:
                                if (this.FoundInfo != null)
                                {
                                    this.FoundInfo(new FoundInfoEventArgs(info));
                                }
                            }
                        }
                    }

                    if (this.searchParams.IncludeSubDirsChecked)
                    {
                        var subDirInfos = dirInfo.GetDirectories();
                        foreach (var subDirInfo in subDirInfos)
                        {
                            if (this.stopped)
                            {
                                break;
                            }

                            // Recursion:
                            this.SearchDirectory(subDirInfo);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private bool MatchesRestrictions(FileSystemInfo info)
        {
            var matches = true;

            if (matches && this.searchParams.NewerThanChecked)
            {
                matches = info.LastWriteTime >= this.searchParams.NewerThanDateTime;
            }

            if (matches && this.searchParams.OlderThanChecked)
            {
                matches = info.LastWriteTime <= this.searchParams.OlderThanDateTime;
            }

            if (matches && this.searchParams.ContainingChecked)
            {
                matches = false;
                if (info is FileInfo)
                {
                    matches = this.FileContainsBytes(info.FullName, this.containingBytes);
                }
            }

            return matches;
        }

        private bool FileContainsBytes(string path, byte[] compare)
        {
            var contains = false;

            var blockSize = 4096;
            if ((compare.Length >= 1) && (compare.Length <= blockSize))
            {
                var block = new byte[compare.Length - 1 + blockSize];

                try
                {
                    var fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                    // Read the first bytes from the file into "block":
                    var bytesRead = fs.Read(block, 0, block.Length);

                    do
                    {
                        // Search "block" for the sequence "compare":
                        var endPos = bytesRead - compare.Length + 1;
                        for (var i = 0; i < endPos; i++)
                        {
                            // Read "compare.Length" bytes at position "i" from the buffer,
                            // and compare them with "compare":
                            int j;
                            for (j = 0; j < compare.Length; j++)
                            {
                                if (block[i + j] != compare[j])
                                {
                                    break;
                                }
                            }

                            if (j == compare.Length)
                            {
                                // "block" contains the sequence "compare":
                                contains = true;
                                break;
                            }
                        }

                        // Search completed?
                        if (contains || (fs.Position >= fs.Length))
                        {
                            break;
                        }
                        else
                        {
                            // Copy the last "compare.Length - 1" bytes to the beginning of "block":
                            for (int i = 0; i < (compare.Length - 1); i++)
                            {
                                block[i] = block[blockSize + i];
                            }

                            // Read the next "blockSize" bytes into "block":
                            bytesRead = compare.Length - 1 + fs.Read(block, compare.Length - 1, blockSize);
                        }
                    }
                    while (!this.stopped);

                    fs.Close();
                }
                catch (Exception)
                {
                }
            }

            return contains;
        }
    }
}