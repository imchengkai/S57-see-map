// Copyright 2009 John Diss www.newgrove.com
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 


using System;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace EasyMap.Extensions.Data
{
    public static class FwToolsHelper
    {
        private static Color[] _TransParentColors;

        public static Color[] TransParentColors
        {
            get { return FwToolsHelper._TransParentColors; }
            set { FwToolsHelper._TransParentColors = value; }
        }

        private static void SetTransParentColors()
        {
            int index = 1;
            string data = "";
            string filename = "TransParentColor.ini";
            string key="TIF_TransParentColors";
            string section="Color"+index;
            data = Common.IniReadValue(filename, key, section);
            List<Color> colors = new List<Color>();
            while (data != "")
            {
                string[] list = data.Split(',');
                if (list.Length == 3)
                {
                    int r, g, b;
                    if (Int32.TryParse(list[0].Trim(), out r)
                        && Int32.TryParse(list[1].Trim(), out g)
                        && Int32.TryParse(list[2].Trim(), out b))
                    {
                        colors.Add(Color.FromArgb(r,g,b));
                    }
                }
                index++;
                section = "Color" + index;
                data = Common.IniReadValue(filename, key, section);
            }
            Color[] cols=new Color[colors.Count];
            colors.CopyTo(cols);
            TransParentColors=cols;
        }

        static FwToolsHelper()
        {
            SetTransParentColors();
            string fwtoolsPath = AppDomain.CurrentDomain.BaseDirectory + "TiffSupport";// @"C:\Program Files\FWTools2.4.7\bin";// ConfigurationManager.AppSettings["FWToolsBinPath"];

            if (String.IsNullOrEmpty(fwtoolsPath) || !Directory.Exists(fwtoolsPath))
                throw new FwToolsPathException(fwtoolsPath);


            string path = Environment.GetEnvironmentVariable("PATH");

            string[] paths = path.Split(new[] { ';', ',' });

            bool pathFound = false;
            foreach (string pth in paths)
            {
                if (String.Compare(pth, fwtoolsPath, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    pathFound = true;
                    break;
                }
            }
            if (!pathFound)
                //Environment.SetEnvironmentVariable("PATH", path + (!path.EndsWith(";") ? ";" : "") + fwtoolsPath);
                Environment.SetEnvironmentVariable("PATH", fwtoolsPath + ";" + path);

            //SetFWToolsEnvironmentVariable(@"C:\Program Files\FWTools2.4.7\proj_lib", "PROJ_LIB");
            ////SetFWToolsEnvironmentVariable("FWToolsGeoTiffCsv", "GEOTIFF_CSV");
            //SetFWToolsEnvironmentVariable(@"C:\Program Files\FWTools2.4.7\data", "GDAL_DATA");
            ////SetFWToolsEnvironmentVariable("FWToolsGdalDriver", "GDAL_DRIVER");

        }

        private static void SetFWToolsEnvironmentVariable(String setting, String envVariable)
        {
            //string set = ConfigurationManager.AppSettings[setting];
            //if (String.IsNullOrEmpty(set))
            //    System.Diagnostics.Debug.WriteLine(string.Format(
            //                                           "\nValue for environment variable '{0}' not set!\nPlease add\n\t<add key=\"{1}\" value=\"...\"/>\n to your app.config file",
            //                                           envVariable, setting));

            Environment.SetEnvironmentVariable(envVariable, setting);
        }

        public static string FwToolsVersion
        {
            get { return "2.4.7"; }
        }

        public static void Configure()
        {
            //does nothing but ensure that the Static initializer has been called.
        }

        #region Nested type: OsGeo4WPathException

        public class FwToolsPathException : Exception
        {
            public FwToolsPathException(string path)
                : base(
                    string.Format("'{0}' is an Invalid Path to FWTools{1}. Create an application setting in [app|web].config key='FWToolsBinPath' pointing to the bin directory of FWTools{1} (absolute file path) . FWTools is downloaded from http://home.gdal.org/fwtools/",
                                  path, FwToolsVersion))
            {
            }
        }

        #endregion
    }
}