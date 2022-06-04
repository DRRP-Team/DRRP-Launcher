using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRRP_Launcher {
    public class DrrpVersion {
        public string name;
        public string url;
        public string foldername;
        public bool forceDownload;

        public DrrpVersion(string _name, string _url, string _foldername, bool _forceDownload) {
            name = _name;
            url = _url;
            foldername = _foldername;
            forceDownload = _forceDownload;
        }
    }

    public class EngineVersion {
        public string name;
        public string url;
        public string foldername;
        public string binaryname;

        public EngineVersion(string _name, string _url, string _foldername, string _binaryname) {
            name = _name;
            url = _url;
            foldername = _foldername;
            binaryname = _binaryname;
        }
    }

    public class Pack {
        public string name;
        public string notes;
        public string engine;
        public string drrp;
        
        public Pack(string _name, string _engine, string _drrp, string _notes) {
            name = _name;
            engine = _engine;
            drrp = _drrp;
            notes = _notes;
        }
    }
}
