using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSheetsDesktopApp
{
    public class GoogleHelper
    {
        private readonly string _token;
        private readonly string _sheet;
        public readonly string AppName = "TestingModule";

        public GoogleHelper(string token, string sheet)
        {
            _token = token;
            _sheet = sheet;
        }
    }
}
