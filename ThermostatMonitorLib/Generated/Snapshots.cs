﻿
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ThermostatMonitorLib
{
    [Serializable]
    public partial class Snapshots : System.Collections.Generic.List<Snapshot>
    {

        #region Constructor
        public Snapshots()
        {
        }
        #endregion

        #region Methods
        public static Snapshots LoadSnapshots(string sql, System.Data.CommandType commandType, MySqlParameter[] parameters)
        {
            return Snapshots.ConvertFromDT(Utils.ExecuteQuery(sql, commandType, parameters));
        }

        public static Snapshots ConvertFromDT(DataTable dt)
        {
            Snapshots result = new Snapshots();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(Snapshot.GetSnapshot(row));
            }
            return result;
        }

        public static Snapshots LoadAllSnapshots()
        {
            return Snapshots.LoadSnapshots("snapshots_load_all", CommandType.StoredProcedure, null);
        }

        public Snapshot GetSnapshotById(int snapshotId)
        {
            foreach (Snapshot snapshot in this)
            {
                if (snapshot.Id == snapshotId) return snapshot;
            }
            return null;
        }

        public static Snapshots LoadSnapshotsByThermostatId(int thermostatId)
        {
            return Snapshots.LoadSnapshots("snapshots_load_by_thermostat_id", CommandType.StoredProcedure, new MySqlParameter[] { new MySqlParameter("@thermostat_id", thermostatId) });
        }


        public Snapshots Sort(string column, bool desc)
        {
            var sortedList = desc ? this.OrderByDescending(x => x.GetPropertyValue(column)) : this.OrderBy(x => x.GetPropertyValue(column));
            Snapshots result = new Snapshots();
            foreach (var i in sortedList) { result.Add((Snapshot)i); }
            return result;
        }

        #endregion

    }
}


