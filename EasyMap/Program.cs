// Copyright 2007 - Rory Plaire (codekaizen@gmail.com)
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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace EasyMap
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            SqlConnection conn = SqlHelper.GetConnection();
            while (true)
            {
                try
                {
                    conn = SqlHelper.GetConnection();
                    conn.Open();

                    conn.Close();
                    break;
                }
                catch(Exception ex)
                {
                    if (MessageBox.Show("数据库连接失败，请检查网络连接是否畅通、数据库连接配置是否正确。", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                    {
                        return;
                    }
                    SqlHelper.Reload();
                }
            }
            //Report.ReportForm1 a = new Report.ReportForm1();
            //a.MapId = 63;
            //Application.Run(a);
            //return;
            MainForm form = new MainForm();
            if (!form.IsDisposed)
            {
                Application.Run(form);
            }
		}
	}
}