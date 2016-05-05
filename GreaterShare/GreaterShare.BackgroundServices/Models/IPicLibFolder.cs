using System;
using System.Collections.Generic;

namespace GreaterShare.BackgroundServices.Models
{
    public interface IPicLibFolder 
    {
        int FileCount { get; set; }
        IList<IPicLibFolder> Folders { get; set; }
        String LastFileEditTime { get; set; }
        string UriString { get; set; }
        bool Equals(IPicLibFolder other);

    }
}