using System;
using System.Collections.Generic;
using System.Text;

namespace gestaosala.core
{
    public static class AppSettings
    {
        #region Apis

        public static class Apis
        {
            public static Uri Usuario => new Uri("https://localhost:4001/api/");
        }

        #endregion
    }
}
