using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PXD010704dll;
using log4net;
using System.Security.Permissions;

[assembly:FileIOPermissionAttribute(SecurityAction.RequestMinimum, AllFiles=FileIOPermissionAccess.AllAccess)]
namespace Bling.Repository.Calyx
{
    public interface IPointDao : IDisposable
    {
        void UpdateField(object field, object value);
        void OpenPExportFile(string pointFile);
    }

    public class PointDao : IPointDao, IDisposable
    {
        private PXD017004 _Point;
        private string _PointFile;
        private ILog _Logger;

        public PointDao()
        {
            _Logger = LogManager.GetLogger(typeof(PointDao)); 
        }

        public void OpenPExportFile(string pointFile)
        {
            bool locked = false;
            int readto = 0;
            _PointFile = pointFile;

            Authorize();

            _Logger.DebugFormat("Reading file {0}", pointFile);
            
            _Point.ReadFile(ref _PointFile, ref locked, ref readto);
            
            if (_Point.LastError != 0)
            {
                _Logger.DebugFormat("Point Error - {0} - {1}", _Point.LastErrorDesc, _Point.ReadErrorDesc);
            }
        }

        public void UpdateField(object field, object value)
        {
            try
            {
                _Logger.DebugFormat("Updating {0} to {1}", field, value);
                _Point.set_Field(ref field, ref value);
            }
            catch (Exception ex)
            {
                _Logger.DebugFormat("Point Error - {0}", ex.Message);
            }
            
        }

        private void Authorize()
        {
            string key = "03E9-7390-0101-76E0-13FA";
            string serial = "19854";

            _Logger.Debug("Authorizing Point");

            _Point = new PXD017004();

            if (!_Point.Authorize(ref key, ref serial))
            {
                _Logger.DebugFormat("Point Error - {0}", _Point.LastErrorDesc);
            }
        }



        public void Dispose()
        {
            bool recalc = true;
            object pointfile = (object)_PointFile;
            _Point.Recalc();
            _Point.WriteFile(ref pointfile, ref recalc);
            if (_Point.LastError != 0)
            {
                _Logger.DebugFormat("Point Error - {0}", _Point.LastErrorDesc);
            }
        }

    }
}
