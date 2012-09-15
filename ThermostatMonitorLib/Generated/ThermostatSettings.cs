using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class ThermostatSettings : System.Collections.Generic.List<ThermostatSetting>
    {

        #region Constructor
        public ThermostatSettings()
        {
        }
        #endregion

        #region Methods
        public static ThermostatSettings LoadThermostatSettings(string sql, System.Data.CommandType commandType, System.Data.SqlClient.SqlParameter[] parameters)
        {
            return ThermostatSettings.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static ThermostatSettings ConvertFromDT(DataTable dt)
        {
            ThermostatSettings result = new ThermostatSettings();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ThermostatSetting.GetThermostatSetting(row));
            }
            return result;
        }

        public static ThermostatSettings LoadAllThermostatSettings()
        {
            return ThermostatSettings.LoadThermostatSettings("LoadThermostatSettingsAll", CommandType.StoredProcedure, null);
        }

        public ThermostatSetting GetThermostatSettingById(int thermostatSettingId)
        {
            foreach (ThermostatSetting thermostatSetting in this)
            {
                if (thermostatSetting.Id == thermostatSettingId) return thermostatSetting;
            }
            return null;
        }

        public static ThermostatSettings LoadThermostatSettingsByThermostatId(System.Int32 thermostatId)
        {
            return ThermostatSettings.LoadThermostatSettings("LoadThermostatSettingsByThermostatId", CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@ThermostatId", thermostatId) });
        }


        public ThermostatSettings Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            ThermostatSettings result = new ThermostatSettings();
            foreach (var i in sortedList) { result.Add((ThermostatSetting)i); }
            return result;
        }

        #endregion


    }
}

