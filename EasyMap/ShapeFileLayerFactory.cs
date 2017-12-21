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
using System.Text;

using EasyMap.Layers;
using EasyMap.Data.Providers;

namespace EasyMap
{
	public class ShapeFileLayerFactory : ILayerFactory
	{
		#region ILayerFactory Members

		public ILayer Create(string layerName, string connectionInfo,VectorLayer.LayerType layertype)
		{
			ShapeFile shapeFileData = new ShapeFile(connectionInfo);
			VectorLayer shapeFileLayer = new VectorLayer(layerName, shapeFileData,layertype);
			return shapeFileLayer;
		}

		#endregion
	}
}
