using System;
using System.Collections.Generic;
using System.IO;

namespace AIDIALabs.IO
{
    public static class Folder
    {
        private static Stack<string> pathStack;

        static Folder()
        {
            pathStack = new Stack<string>();
        }

        private static void CreateFolderPath(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    pathStack.Push(path);
                    DirectoryInfo parent = Directory.GetParent(path);
                    if (parent != null)
                    {
                        CreateFolderPath(parent.FullName);
                    }
                }
                else if (pathStack.Count == 0)
                {
                    Directory.CreateDirectory(path);
                }
                else
                {
                    int count = pathStack.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Directory.CreateDirectory(pathStack.Pop());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void CreateFolder(string path)
        {
            pathStack.Clear();
            CreateFolderPath(path);
        }
    }
}
