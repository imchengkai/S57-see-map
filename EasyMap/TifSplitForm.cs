using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OSGeo.GDAL;
using EasyMap.Extensions.Data;

namespace EasyMap
{
    public partial class TifSplitForm : MyForm
    {
        public TifSplitForm()
        {
            InitializeComponent();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (!Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                Directory.CreateDirectory(folderBrowserDialog1.SelectedPath);
            }
            progressBar1.Visible = true;
            int w = 0;
            int h = 0;
            int wcount = 0;
            int hcount = 0;
            int row = (int)numRow.Value;
            int col = (int)numCol.Value;
            FwToolsHelper.Configure();
            Gdal.AllRegister();
            //if (row == 1 && col == 1)
            //{
            //    CreatePyramids(textBox1.Text);
            //    return;
            //}
            progressBar1.Maximum = row * col;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            
            Dataset ds=Gdal.Open(textBox1.Text, Access.GA_ReadOnly);
            w = ds.RasterXSize / col;
            h = ds.RasterYSize / row;
            string path=folderBrowserDialog1.SelectedPath;
            if(!path.EndsWith("\\"))
            {
                path+="\\";
            }
            
            string mainfilename = Path.GetFileNameWithoutExtension(textBox1.Text);
            string filename = "";
            int i = 0;
            int j = 0;
            for (i = 0; i < row; i++)
            {
                wcount = 0;
                for (j = 0; j < col; j++)
                {
                    filename = path + mainfilename + "_" + i + "_" + j + ".tif";
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    progressBar1.Value++;
                    Driver dr = Gdal.GetDriverByName("GTiff");
                    Dataset dest = dr.Create (filename, w, h, ds.RasterCount, DataType.GDT_Byte, new string[]{"INTERLEAVE=PIXEL"});

                    for (int k = 1; k <= ds.RasterCount; k++)
                    {
                        byte[] data = new byte[w * h];
                        Band band=ds.GetRasterBand(k);
                        Band destband = dest.GetRasterBand(k);
                        band.ReadRaster(wcount, hcount, w, h, data, w, h, 0, 0);
                        destband.WriteRaster(0, 0, w, h, data, w, h,0, 0);
                    }
                    dest.SetGeoTransform(GDALInfoGetPosition(ds, wcount, hcount));
                    dest.FlushCache();
                    dest.Dispose();
                    dr.Dispose();
                    CreatePyramids(filename);
                    wcount += w;
                }
                if (wcount < ds.RasterXSize)
                {
                    filename = path + mainfilename + "_" + i + "_" + j + ".tif";
                    int ww = ds.RasterXSize - wcount;
                    Driver dr = Gdal.GetDriverByName("GTiff");
                    Dataset dest = dr.Create(filename, ww, h, ds.RasterCount, DataType.GDT_Byte, new string[] { "INTERLEAVE=PIXEL" });
                    for (int k = 1; k <= ds.RasterCount; k++)
                    {
                        byte[] data = new byte[ww * h];
                        Band band = ds.GetRasterBand(k);
                        Band destband = dest.GetRasterBand(k);
                        band.ReadRaster(wcount, hcount, ww, h, data, ww, h,0, 0);
                        destband.WriteRaster(0, 0, ww, h, data, ww, h, 0, 0);
                    }
                    dest.SetGeoTransform(GDALInfoGetPosition(ds, wcount, hcount));
                    dest.FlushCache();
                    dest.Dispose();
                    dr.Dispose();
                    CreatePyramids(filename);
                }
                hcount += h;
            }
            if (hcount < ds.RasterYSize)
            {
                h = ds.RasterYSize - hcount;
                wcount = 0;
                for (j = 0; j < col; j++)
                {
                    filename = path + mainfilename + "_" + i + "_" + j + ".tif";
                    
                    Driver dr = Gdal.GetDriverByName("GTiff");
                    Dataset dest = dr.Create(filename, w, h, ds.RasterCount, DataType.GDT_Byte, null);
                    for (int k = 1; k < ds.RasterCount; k++)
                    {
                        byte[] data = new byte[w * h];
                        Band band = ds.GetRasterBand(k);
                        Band destband = dest.GetRasterBand(k);
                        band.ReadRaster(wcount, hcount, w, h, data, w, h, 0, 0);
                        destband.WriteRaster(0, 0, w, h, data, w, h, 0, 0);
                    }
                    dest.SetGeoTransform(GDALInfoGetPosition(ds, wcount, hcount));
                    dest.FlushCache();
                    dest.Dispose();
                    dr.Dispose();
                    CreatePyramids(filename);
                    wcount += w;
                }
                if (wcount < ds.RasterXSize)
                {
                    filename = path + mainfilename + "_" + i + "_" + j + ".tif";
                    w = ds.RasterXSize - wcount;
                    
                    Driver dr = Gdal.GetDriverByName("GTiff");
                    Dataset dest = dr.Create(filename, w, h, ds.RasterCount, DataType.GDT_Byte, null);
                    for (int k = 1; k < ds.RasterCount; k++)
                    {
                        byte[] data = new byte[w * h];
                        Band band = ds.GetRasterBand(k);
                        Band destband = dest.GetRasterBand(k);
                        band.ReadRaster(wcount, hcount, w, h, data, w, h, 0, 0);
                        destband.WriteRaster(0, 0, w, h, data, w, h, 0, 0);
                    }
                    dest.SetGeoTransform(GDALInfoGetPosition(ds, wcount, hcount));
                    dest.FlushCache();
                    dest.Dispose();
                    dr.Dispose();
                    CreatePyramids(filename);
                }
            }
            
        }

        private static double[] GDALInfoGetPosition(Dataset ds, double x, double y)
        {
            double[] d = new double[6];
            d[0] = 41365664.6713316;
            d[1] = 0.5;
            d[2] = 0;
            d[3] = 4393878.74722208;
            d[4] = 0;
            d[5] = -0.5;
            return d;
            double[] adfGeoTransform = new double[6];
            ds.GetGeoTransform(adfGeoTransform);

            adfGeoTransform[0] = adfGeoTransform[0] + adfGeoTransform[1] * x + adfGeoTransform[2] * y;
            adfGeoTransform[3] = adfGeoTransform[3] + adfGeoTransform[4] * x + adfGeoTransform[5] * y;
            return adfGeoTransform;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CreatePyramids(string filename)
        {
            Gdal.SetConfigOption("USE_RRD", "YES");
            Dataset ds = Gdal.Open(filename, Access.GA_Update);
            Driver drv = ds.GetDriver();
            // System.Type szDriver = drv.ShortName.GetType();
            int iWidth = ds.RasterXSize;
            int iHeight = ds.RasterYSize;
            int iPixelNum = iWidth * iHeight;    //图像中的总像元个数 
            int iTopNum = 4096;                  //顶层金字塔大小，64*64
            int iCurNum = iPixelNum / 4;

            int[] anLevels = new int[1024];
            int nLevelCount = 0;                 //金字塔级数
            do
            {
                anLevels[nLevelCount] = Convert.ToInt32(Math.Pow(2.0, nLevelCount + 2));
                nLevelCount++;
                iCurNum /= 4;
            } while (iCurNum > iTopNum);

            int[] levels = new int[nLevelCount];
            for (int a = 0; a < nLevelCount; a++)
            {
                levels[a] = anLevels[a];
            }
            int ret = ds.BuildOverviews("nearest", levels);
            ds.Dispose();
            drv.Dispose();
            return true;
        }
    }
}
