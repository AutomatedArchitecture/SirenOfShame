using System;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class AudioPatternSetting
    {
        private string _name;

        public string FileName { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (_name != null && _name.Length > 20)
                {
                    _name = _name.Substring(0, 20);
                }
            }
        }
    }
}
