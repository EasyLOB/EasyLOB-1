namespace EasyLOB.Extensions.Ini
{
    /// <summary>
    /// INI Manager - Mock
    /// </summary>
    public partial class IniManagerMock : IIniManager
    {
        #region Methods Interface

        public bool Write(string section, string key, string value)
        {
            return true;
        }

        public string Read(string section, string key)
        {
            return "";
        }

        public bool DeleteKey(string section, string key)
        {
            return true;
        }

        public bool DeleteSection(string section)
        {
            return true;
        }

        #endregion Methods Interface
    }
}